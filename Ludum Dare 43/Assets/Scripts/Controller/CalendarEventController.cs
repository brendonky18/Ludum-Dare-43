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

    private Dictionary<Date, List<CalendarEvent>> allEvents;
    private List<CalendarEvent> allEventsToday;

    //set up singleton
    void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("Something is already assigned to CalendarEventController.Instance");
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //adds new events to keep track of
    public void AddEvent(CalendarEvent eventToAdd) {
        List<CalendarEvent> list;

        if (!allEvents.TryGetValue(eventToAdd.StartDate, out list)) {
            list = new List<CalendarEvent>();
            allEvents.Add(eventToAdd.StartDate, list);
        }

        list.Add(eventToAdd);
    }

    public List<CalendarEvent> GetEventsOnDay(Date date) {
        return allEvents[date];
    }

    public List<CalendarEvent> GetEventsToday() {
        return allEvents[Today.Instance.Date];
    }
}
