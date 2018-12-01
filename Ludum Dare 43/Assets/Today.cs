using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Today : MonoBehaviour {
    public GameObject calendarFrame;

    public Date defaultStartDate = Calendar.JUNIOR_YEAR_HS_START;//by default starts on the first day of Junior year

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
        protected set { time = value; }
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
    }

    //initializes current date and time
    void Start() {
        time = Time.Midnight;
        Date = defaultStartDate;
        Date = defaultStartDate.Tomorrow;
        Date = new Date(2020, 2, 29);
        Date = date.Tomorrow;
    }


    // Update is called once per frame

}
