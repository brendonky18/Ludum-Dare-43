using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class World : MonoBehaviour {
    //<Singleton>
    private static World instance;
    public static World Instance {
        get { return instance; }
        protected set { instance = value; }
    }
    //</Singleton>

    //<Today>

    //GameObject References
    public GameObject playerController;
    public GameObject calendarFrame;
    public GameObject phoneGO;
    public GameObject clockText;
    public GameObject dateText;

    private Date date; //current date
    private Time time; //current time
    public Date Date {
        get { return date; }
        protected set {
            Debug.Log("Changing date to: " + value.ToString());
            if (value != date) {
                date = value;
                onDayChange();
            }
            date = value;//this is probably redundant, but just to be safe
        }
    }//Always use this, it has the callback
    public Time Time {
        get { return time; }
        protected set {
            time = value;
        }
    }
    public Date defaultStartDate = Calendar.JUNIOR_YEAR_HS_START.Yesterday;//by default starts on the first day of Junior year

    private Dictionary<Date, List<CalendarEvent>> allEvents = new Dictionary<Date, List<CalendarEvent>>();
    private List<CalendarEvent> eventsStartingToday = new List<CalendarEvent>();
    private List<CalendarEvent> eventsEndingToday = new List<CalendarEvent>();
    private List<CalendarEvent> ongoingEvents = new List<CalendarEvent>(); //all events which haven't ended yet

    /// <summary>
    /// takes care of what should happen when the day changes
    /// updates calendar UI in class CalendarLayout
    /// prints to Debug log in class Today
    /// calls UpdateEventsToday() in CalendarEventController
    /// </summary>
    private Action onDayChange; //use this, this class is the only one who should call it anyways
    public Action OnDayChange {
        set {
            onDayChange += value;
        }
    }//no getter, only setter
    private Action changeToTomorrow;//advances the day to tomorrow

    //initializes all the variables
    private void Awake() {
        //Initializes Singleton
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("There's already something assigned to World.instance");
        }

        //initializes callbacks
        changeToTomorrow = () => {
            Date = date.Tomorrow;
        };
        OnDayChange = () => {
            //updates the calendar UI if the year or month has changed, of it it hasn't been initialized yet
            calendarFrame.GetComponent<CalendarLayout>().DisplayDate(Date);
        };
        OnDayChange = () => {
            ProcessEvents();
        };
        OnDayChange = () => {
            dateText.GetComponent<Text>().text = Date.ToString();
        };


        Date = defaultStartDate;
        //initializes other variables
        Time = Time.Midnight;
        //initializes events
        InitEvents();
        Instance.onDayChange();
    }

    private Time WakeupTime = new Time(7);
    private void Start() {
        
    }

    private int tickSpeed = 1;//672;
    public int TickSpeed {
        get { return tickSpeed; }
        protected set { tickSpeed = value; }
    }
    // Update is called once per frame
    void Update() {
        //how many in-game seconds elaspe each irl second
        UpdateTime(TickSpeed);
    }

    private float deltaTime = 0;
    private void UpdateTime(int tickSpeed) {
        deltaTime += UnityEngine.Time.deltaTime;

        if (deltaTime >= 1f / tickSpeed) {
            World.Instance.Time.IncrementTimeByHours(1, changeToTomorrow);//this should be the only place where onDayChanged can be called
            deltaTime -= 1f / tickSpeed;
        }

        clockText.GetComponent<Text>().text = Instance.Time.TwelveHourTime.ToString() + (Instance.Time.TwelveHourTime.isPM ? " PM" : " AM");
    }

    public void ProcessEvents() {
        Debug.Log("ProcessEvents()");
        eventsStartingToday = GetEventsStartingOnDay(World.Instance.Date);

        Debug.Log(eventsStartingToday.Count + " events starting today");
        //start the events which need to be started
        foreach (CalendarEvent curEvent in eventsStartingToday) {
            Debug.Log(curEvent.ToString() + " is starting today");
            if (World.Instance.Time.IsGreaterThan(curEvent.StartTime)) {
                ongoingEvents.Add(curEvent);//add to ongoing events
                curEvent.StartEvent();
            }
        }
        //remove all events from eventsStartingToday if they have already started
        eventsStartingToday.RemoveAll((curEvent) => { return World.Instance.Time.IsGreaterThan(curEvent.StartTime); });//is it possible to stick the previous foreach loop inside removeAll's predicate. Probably shouldn't since it doesn't say to to that anywhere

        /************************ mirriored **********************/

        //end the events that need to be ended
        foreach (CalendarEvent curEvent in eventsEndingToday) {
            if (World.Instance.Time.IsGreaterThan(curEvent.EndTime)) {
                ongoingEvents.Remove(curEvent);//remove from ongoing events
                curEvent.EndEvent();
            }
        }
        //remove all events from eventsEndingToday if they have already ended
        eventsEndingToday.RemoveAll((curEvent) => { return World.Instance.Time.IsGreaterThan(curEvent.EndTime); });
    }
    //adds new events to keep track of
    public void AddEvent(CalendarEvent eventToAdd) { ///Event being added on start
        List<CalendarEvent> list;

        if (!allEvents.TryGetValue(eventToAdd.StartDate, out list)) {
            list = new List<CalendarEvent>();
            allEvents.Add(eventToAdd.StartDate, list);
        }

        list.Add(eventToAdd);

        if(eventToAdd.StartDate.MONTH == Instance.Date.MONTH) {
            //update calendar
        }
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
        eventsStartingToday = GetEventsStartingOnDay(World.Instance.Date);
    }


    //initialize events
    PhoneController pc;
    void InitEvents() {
        CalendarEvent firstDayOfSchool = new CalendarEvent();
        CalendarEvent testEvent = new CalendarEvent();
        CalendarEvent wakeUp = new CalendarEvent();

        pc = phoneGO.GetComponent<PhoneController>();

        firstDayOfSchool
            .SetDate(Calendar.JUNIOR_YEAR_HS_START)
            .SetTime(new Time(7, 45))
            .SetEventName("First Day of School")
            .RegisterEventAction();

        testEvent
            .SetDate(Calendar.JUNIOR_YEAR_CO_START)
            .SetTime(Time.Midnight)
            .SetEventName("Test Event")
            .RegisterEventAction(() => {
                pc.ReceiveTextNotification(TextMessage.Messages["Test Message"]);
                pc.ReceiveTextNotification(TextMessage.Messages["Test Message"]);
            }, null)
            .SetIsHidden(true);
        wakeUp
            .SetDate(Instance.defaultStartDate)
            .SetTime(WakeupTime)
            .SetEventName("Alarm")
            .SetIsHidden(true)
            .RegisterEventAction(() => {
                pc.ReceiveNotification(
                    new Notification(
                        "Alarm",
                        "Tap to stop",
                        () => {
                            wakeUp = wakeUp.SetDate(World.Instance.Date.Tomorrow).SetTime(WakeupTime);
                            World.Instance.AddEvent(wakeUp);
                        }
                    )
                );
            }, null);



        World.Instance.AddEvent(firstDayOfSchool);
        World.Instance.AddEvent(wakeUp);
        //World.Instance.AddEvent(testEvent);
    }
}
