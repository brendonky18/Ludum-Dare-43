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
            Debug.Log("Displaying date: " + value.ToString());

            //updates the calendar UI if the year or month has changed, of it it hasn't been initialized yet
            if (date == null || value.YEAR != date.YEAR || value.MONTH != date.MONTH)
                calendarFrame.GetComponent<CalendarLayout>().DisplayDate(value);

            date = value;
        }
    }//Always use this, it will update the calendar display
    public Time Time {
        get { return time; }
        protected set {
            Debug.Log("Time: " + value.ToString());
            time = value;
        }
    }

    //callback for when the date changes to the next day
    private Action onDayChange;
    public Action OnDayChange {
        set {
            onDayChange += value;
        }
    }    

    //gets called before anything else
    void Awake() {
        //initializes the singleton instance
        if (Today.Instance == null)
            Today.Instance = this;
        else
            Debug.LogError("Something is already assigned to Today.Instance");

        //update the date when the day changes
        OnDayChange = () => {
            Date = date.Tomorrow;
        };
    }

    //initializes current date and time
    void Start() {
        Time = Time.Midnight;
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
            Today.Instance.Time.IncrementTimyBySeconds(timeIncrement, onDayChange);
            Debug.Log(Today.Instance.Time.ToString());
            deltaTime--;
        }
    }
}
