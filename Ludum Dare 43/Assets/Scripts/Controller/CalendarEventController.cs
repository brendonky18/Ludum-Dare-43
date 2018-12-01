using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used to keep track of and trigger all the various events
public class CalendarEventController : MonoBehaviour {
    private static CalendarEventController instance;
    public static CalendarEventController Instance {
        get { return instance; }
        protected set { instance = value; }
    }

    private Dictionary<Date, List<CalendarEvent>> allEvents = new Dictionary<Date, List<CalendarEvent>>();
    private List<CalendarEvent> eventsStartingToday = new List<CalendarEvent>();
    private List<CalendarEvent> eventsEndingToday = new List<CalendarEvent>();
    private List<CalendarEvent> ongoingEvents = new List<CalendarEvent>(); //all events which haven't ended yet

    //set up singleton
    void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("Something is already assigned to CalendarEventController.Instance");
        }

        //register callback which updates which events starting today
        Today.Instance.OnDayChange = UpdateEventsToday;
    }

    // Use this for initialization
    // initialize events
    
	
	// Update is called once per frame
	void Update () {
        //TODO: Change this to only be called when the Today.time is updated
        ProcessEvents();
	}

    //adds new events to keep track of
    public void AddEvent(CalendarEvent eventToAdd) { ///Event being added on start
        List<CalendarEvent> list;
        
        if (!allEvents.TryGetValue(eventToAdd.StartDate, out list)) {
            list = new List<CalendarEvent>();
            allEvents.Add(eventToAdd.StartDate, list);
        }

        list.Add(eventToAdd);
    }

    public List<CalendarEvent> GetEventsStartingOnDay(Date date) {
        List<CalendarEvent> list;
        allEvents.TryGetValue(date, out list);
        if (list == null) {
            return new List<CalendarEvent>();
        } else
            return list;
    }
    public void UpdateEventsToday() {
        eventsStartingToday = GetEventsStartingOnDay(Today.Instance.Date);
    }

    public void ProcessEvents() {
        //start the events which need to be started
        foreach (CalendarEvent curEvent in eventsStartingToday) {
            if (Today.Instance.Time.IsGreaterThan(curEvent.StartTime)) {
                ongoingEvents.Add(curEvent);//add to ongoing events
                curEvent.StartEvent();
            }
        }
        //remove all events from eventsStartingToday if they have already started
        eventsStartingToday.RemoveAll((curEvent) => { return Today.Instance.Time.IsGreaterThan(curEvent.StartTime); });//is it possible to stick the previous foreach loop inside removeAll's predicate. Probably shouldn't since it doesn't say to to that anywhere
        
        /************************ mirriored **********************/

        //end the events that need to be ended
        foreach(CalendarEvent curEvent in eventsEndingToday) {
            if (Today.Instance.Time.IsGreaterThan(curEvent.EndTime)) {
                ongoingEvents.Remove(curEvent);//remove from ongoing events
                curEvent.EndEvent();
            }
        }
        //remove all events from eventsEndingToday if they have already ended
        eventsEndingToday.RemoveAll((curEvent) => { return Today.Instance.Time.IsGreaterThan(curEvent.EndTime); });
    }
}
