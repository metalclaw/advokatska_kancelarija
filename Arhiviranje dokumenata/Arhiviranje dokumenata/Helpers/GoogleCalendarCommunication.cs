using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using MongoDB.Bson;
using Colors = Google.Apis.Calendar.v3.Data.Colors;
using MessageBox = System.Windows.MessageBox;

namespace Arhiviranje_dokumenata.Helpers
{
    class GoogleCalendarCommunication
    {
        static readonly string[] Scopes = { CalendarService.Scope.Calendar };
        private static CalendarService service;
        private static BatchRequest batch;
        private static Events allEvents;
        private static int nrOfCreateEventsInBatch = 0;

        #region create

        public static void createConnection(Form1 parent)
        {
            UserCredential credentials;

            try
            {
                using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    string credPath = "token.json";
                    credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(credPath, true)).Result;
                }
                service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credentials,
                    ApplicationName = "Arhiviranje Dokumenata",
                });

                batch = new BatchRequest(service);

                getEvents();
            }
            catch(Exception e)
            {
                parent.showMessage("Došlo je do greške pri povezivanju sa Google Kalendarom: " + e.Message);
            }
        }

        private static Event createEvent(string id, string summary, string location, DateTime startTime, string[] attendees, string color, bool isBatch)
        {
            Event newEvent = new Event();

            while (id.Length < 5)
            {
                id = "0" + id;
            }

            var eventsForThatDay = getEventsForDay(new DateTime(startTime.Year, startTime.Month, startTime.Day));

            List<int> takenHours = new List<int>();

            foreach (var item in eventsForThatDay.Items)
            {
                if (item.Start.DateTime != null)
                {
                    takenHours.Add(item.Start.DateTime.Value.Hour);
                }
            }

            int hourToUse = -1;

            int repeated = 0;

            for (int i = 0; i < 24; i++)
            {
                if(!takenHours.Contains(i))
                {
                    if (isBatch && repeated < nrOfCreateEventsInBatch)
                    {
                        repeated++;
                        continue;
                    }
                    else
                    {
                        hourToUse = i;
                        break;
                    }
                }
            }

            if (hourToUse == -1)
            {
                hourToUse = 0;
            }

            startTime = startTime.AddHours(hourToUse);
            var endTime = startTime.AddMinutes(30);

            newEvent.Id = id;
            newEvent.Summary = summary;
            newEvent.Location = location;
            newEvent.Start = new EventDateTime() { DateTimeRaw = startTime.ToString(GlobalVariables.google_calendar_datetime_pattern), TimeZone = GlobalVariables.google_calendar_timezone };
            newEvent.End = new EventDateTime() { DateTimeRaw = endTime.ToString(GlobalVariables.google_calendar_datetime_pattern), TimeZone = GlobalVariables.google_calendar_timezone };
            newEvent.Attendees = new EventAttendee[attendees.Length];
            newEvent.ColorId = color;
            newEvent.Reminders = new Event.RemindersData();
            newEvent.Reminders.UseDefault = false;
            newEvent.Reminders.Overrides = new List<EventReminder>();

            if (newEvent.Start.DateTime.Value.Day != newEvent.End.DateTime.Value.Day) {
                var difference = newEvent.End.DateTime.Value.Day - newEvent.Start.DateTime.Value.Day;
                newEvent.End.DateTime = newEvent.Start.DateTime.Value.AddMinutes(10);
            }

            int j = 0;
            foreach (string attendee in attendees)
            {
                newEvent.Attendees[j++] = new EventAttendee() { Email = attendee };
            }

            return newEvent;
        }

        public static void createRocista(Form1 parent, List<Rociste> lista, bool executeNow, string extraText = "")
        {
            var color = GlobalVariables.GoogleCalendarColorRocista;

            if (lista.Count > 0)
            {
                foreach (Rociste item in lista)
                {
                    var actualId = item.id == ObjectId.Empty ? item.Id : item.id;
                    
                    if (getEvent(actualId.ToString()) == null) {
                        addEventToBatch(parent, actualId.ToString(), extraText + " " + item.text, "", item.datum, new string[0], color);
                    }
                    else
                    {
                        updateRocistaAkoPostojeNaKalendaru(parent, lista, executeNow, extraText);
                    }
                }
                if (executeNow)
                {
                    executeBatch();
                }
            }
        }

        public static void createEvidencije(Form1 parent, List<ListaEvidencija> lista, bool executeNow, string extraText = "")
        {
            var color = GlobalVariables.GoogleCalendarColorEvidencije;

            if (lista.Count > 0)
            {
                foreach (ListaEvidencija item in lista)
                {
                    //ako ne treba da bude na kalendaru preskoci
                    if (!item.imaEventNaGoogleKalendaru)
                    {
                        continue;
                    }
                    addEventToBatch(parent, item.id.ToString(), extraText + " " + item.tekstEvidencije, "", item.datum, new string[0], color);
                }

                if (executeNow)
                {
                    executeBatch();
                }
            }
        }

        #endregion

        #region get

        public static Colors getColors()
        {
            return service.Colors.Get().Execute();
        }

        public static Events getEvents()
        {
            EventsResource.ListRequest request = service.Events.List("primary");
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.Updated;

            return allEvents = request.Execute();
        }

        public static Events getEventsForDay(DateTime date)
        {
            EventsResource.ListRequest request = service.Events.List("primary");
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.TimeMin = date.Date;
            request.TimeMax = date.Date.AddDays(1);

            return request.Execute();
        }

        public static Event getEvent(string id)
        {
            EventsResource.GetRequest request = service.Events.Get("primary", id);
            Event result;
            try
            {
                result = request.Execute();
            }
            catch (Exception e)
            {
                result = null;
            }
            return result;
        }

        #endregion

        #region update

        public static void updateEventToBatch(Form1 parent, string id, string summary, string location, DateTime startTime, string[] attendees, string color)
        {
            if (batch.Count < 50)
            {
                while (id.Length < 5)
                {
                    id = "0" + id;
                }

                Event newEvent = createEvent(id, summary, location, startTime, attendees, color, true);
                nrOfCreateEventsInBatch++;
                try
                {
                    batch.Queue<Event>(service.Events.Update(newEvent, "primary", id), (content, error, i, message) =>
                    {
                        if (error != null)
                        {
                            parent.showMessage(error.ToString() + newEvent.ToJson());
                        }
                    });
                }
                catch (Exception e)
                {
                    parent.showMessage(e.Message);
                }
            }
            else
            {
                executeBatch();
                updateEventToBatch(parent, id, summary, location, startTime, attendees, color);
            }
        }

        public static void updateEvent(Form1 parent, string id, string summary, string location, DateTime startTime, string[] attendees, string color)
        {
            if (allEvents.Items != null && allEvents.Items.Count > 0)
            {
                while (id.Length < 5)
                {
                    id = "0" + id;
                }

                Event newEvent = createEvent(id, summary, location, startTime, attendees, color, false);

                try
                {
                    service.Events.Update(newEvent, "primary", id).Execute();
                }
                catch (Exception e)
                {
                    parent.showMessage(e.Message);
                }
            }
            else
            {
                //no events
                parent.showMessage("Nije pronađen ni jedan event!");
            }
        }

        public static void updateEvidencijeAkoPostojeNaKalendaru(Form1 parent, List<ListaEvidencija> lista, bool executeNow)
        {
            var color = GlobalVariables.GoogleCalendarColorEvidencije;

            if (lista.Count > 0)
            {
                foreach (ListaEvidencija item in lista)
                {
                    var evidencija = getEvent(item.id.ToString());
                    if (evidencija != null)
                    {
                        updateEventToBatch(parent, item.id.ToString(), item.tekstEvidencije, "", item.datum, new string[0], color);
                    }
                }

                if (executeNow)
                {
                    executeBatch();
                }
            }
        }

        public static void updateRocistaAkoPostojeNaKalendaru(Form1 parent, List<Rociste> lista, bool executeNow, string extraText = "")
        {
            var color = GlobalVariables.GoogleCalendarColorRocista;

            if (lista.Count > 0)
            {
                foreach (Rociste item in lista)
                {
                    var actualId = item.id == ObjectId.Empty ? item.Id : item.id;
                    var rociste = getEvent(actualId.ToString());
                    if (rociste != null)
                    {
                        updateEventToBatch(parent, actualId.ToString(), extraText + " " + item.text, "", item.datum, new string[0], color);
                    }
                    else {
                        addEventToBatch(parent, actualId.ToString(), extraText + " " + item.text, "", item.datum, new string[0], color);
                    }
                }

                if (executeNow)
                {
                    executeBatch();
                }
            }
        }

        #endregion

        #region delete

        public static void deleteEvent(Form1 parent, string id)
        {
            try
            {
                service.Events.Delete("primary", id);
            }
            catch (Exception e)
            {
                parent.showMessage(e.Message + "\nEvent Id: " + id);
            }
        }

        public static void deleteEvidencije(Form1 parent, List<ListaEvidencija> lista, bool executeNow)
        {
            if (lista.Count > 0)
            {
                foreach (ListaEvidencija item in lista)
                {
                    if (getEvent(item.id.ToString()) != null)
                    {
                        deleteEventToBatch(parent, item.id.ToString());
                    }
                }

                if (executeNow)
                {
                    executeBatch();
                }
            }
        }

        public static void deleteEventToBatch(Form1 parent, string id)
        {
            if (batch.Count < 50)
            {
                while (id.Length < 5)
                {
                    id = "0" + id;
                }

                try
                {
                    batch.Queue<Event>(service.Events.Delete("primary", id), (content, error, i, message) =>
                    {
                        if (error != null)
                        {
                            parent.showMessage(error.ToString() + content?.Id);
                        }
                    });
                }
                catch (Exception e)
                {
                    parent.showMessage(e.Message);
                }
            }
            else
            {
                executeBatch();
                deleteEventToBatch(parent, id);
            }
        }

        public static void deleteRocista(Form1 parent, List<Rociste> lista, bool executeNow)
        {
            if (lista.Count > 0)
            {
                foreach (Rociste item in lista)
                {
                    var actualId = item.id == ObjectId.Empty ? item.Id : item.id;

                    if (getEvent(actualId.ToString()) != null)
                    {
                        deleteEventToBatch(parent, actualId.ToString());
                    }
                    else
                    {
                        //error!
                    }
                }

                if (executeNow)
                {
                    executeBatch();
                }
            }
        }

        #endregion

        #region add

        public static void addEvent(Form1 parent, string id, string summary, string location, DateTime startTime, string[] attendees, string color = "green")
        {
            //reset start time to 12am
            DateTime startTimeReset = startTime.Date;

            Event newEvent = createEvent(id, summary, location, startTimeReset, attendees, color, false);
            try
            {
                service.Events.Insert(newEvent, "primary").Execute();
            }
            catch (Exception ex)
            {
                parent.showMessage(ex.Message + "\nEvent Id: " + newEvent.Id);
            }
        }

        public static void addEventToBatch(Form1 parent, string id, string summary, string location, DateTime startTime, string[] attendees, string color = "green")
        {
            if (batch.Count < 50)
            {
                //reset start time to 12am
                DateTime startTimeReset = startTime.Date;

                Event newEvent = createEvent(id, summary, location, startTimeReset, attendees, color, true);
                nrOfCreateEventsInBatch++;
                //add event to batch
                batch.Queue<Event>(service.Events.Insert(newEvent, "primary"), (content, error, i, message) => {
                    // Put your callback code here.
                    if (error != null)
                    {
                        parent.showMessage(error.ToString() + newEvent.ToJson());
                    }
                });
            }
            else
            {
                //upload this queue, create new one and add request to it
                executeBatch();
                addEventToBatch(parent, id, summary, location, startTime, attendees);
            }
        }

        #endregion

        public static void executeBatch()
        {
            batch.ExecuteAsync();
            batch = new BatchRequest(service);
            nrOfCreateEventsInBatch = 0;
        }
    }
}
