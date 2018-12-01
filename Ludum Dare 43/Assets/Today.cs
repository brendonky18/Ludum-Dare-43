using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Today : MonoBehaviour {
    public GameObject calendarFrame;

    public Date defaultStartDate = Calendar.JUNIOR_YEAR_HS_START.Yesterday;//by default starts on the first day of Junior year

    private static Today instance;
    public static Today Instance {
        get { return instance; }
        protected set { instance = value; }
    }

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

    private Action changeToTomorrow;

    //gets called before anything else
    void Awake() {
        //initializes the singleton instance
        if (Today.Instance == null)
            Today.Instance = this;
        else
            Debug.LogError("Something is already assigned to Today.Instance");

        //does what it says
        changeToTomorrow = () => {
            Date = date.Tomorrow;
        };
        OnDayChange = () => {
            //updates the calendar UI if the year or month has changed, of it it hasn't been initialized yet
            calendarFrame.GetComponent<CalendarLayout>().DisplayDate(Date);
        };
        OnDayChange = () => {
            Debug.Log("OnDayChange()");
        };
    }

    //initializes current date and time
    void Start() {
        Time = Time.EndOfDay;
        Date = defaultStartDate;
    }


    // Update is called once per frame
    void Update() {
        //how many in-game seconds elaspe each irl second
        UpdateTime(60);
    }

    private float deltaTime = 0;
    private void UpdateTime(int timeIncrement) {
        deltaTime += UnityEngine.Time.deltaTime;

        if(deltaTime >= 1) {
            Today.Instance.Time.IncrementTimyBySeconds(timeIncrement, changeToTomorrow);//this should be the only place where onDayChanged can be called
            deltaTime--;
        }
    }
}
