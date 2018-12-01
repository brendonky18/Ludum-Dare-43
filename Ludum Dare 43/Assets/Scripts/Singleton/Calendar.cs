using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps track of the different things you have planned
public class Calendar : MonoBehaviour {
    //start dates for school
    public static readonly Date JUNIOR_YEAR_HS_START = new Date(2019, 9, 3);
    public static readonly Date SENIOR_YEAR_HS_START = new Date(2020, 9, 8);
    public static readonly Date FRESHMAN_YEAR_CO_START = new Date(2021, 9, 7);
    public static readonly Date SOPHOMORE_YEAR_CO_START = new Date(2022, 9, 6);
    public static readonly Date JUNIOR_YEAR_CO_START = new Date(2023, 9, 5);
    public static readonly Date SENIOR_YEAR_CO_START = new Date(2024, 9, 3);

    public static Calendar Instance;

    private static Dictionary<Date, CalendarEvent> events = new Dictionary<Date, CalendarEvent>();

    //Called before anything else
    private void Awake() {
        //initializes the singleton instance
        if (Calendar.Instance == null)
            Calendar.Instance = this;
        else
            Debug.LogError("Something is already assigned to Calendar.Instance");
    }

    public void AddEvent(CalendarEvent newEvent){
        events.Add(newEvent.StartDate, newEvent);
    }

    
}
