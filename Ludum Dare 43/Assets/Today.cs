using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Today : MonoBehaviour {
    private static Today instance;
    public static Today Instance {
        get { return instance; }
        protected set { instance = value; }
    }

    private Date date; //current date
    private Time time; //current time
    public Date Date {
        get { return date; }
        protected set { date = value; }
    }
    public Time Time {
        get { return time; }
        protected set { time = value; }
    }

    //callback for when the date changes to the next day
    private Action onDayChange;

    public Date defaultStartDate = Calendar.JUNIOR_YEAR_HS_START;//by default starts on the first day of sophomore year

    //gets called before anything else
    private void Awake() {
        //initializes the singleton instance
        if (Today.Instance == null)
            Today.Instance = this;
        else
            Debug.LogError("Something is already assigned to Today.Instance");
    }

    //initializes current date and time

    // Update is called once per frame
    
}
