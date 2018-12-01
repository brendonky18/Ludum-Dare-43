using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnualCalendarEvent : CalendarEvent {

    public AnnualCalendarEvent(CalendarEvent calendarEvent) : base(calendarEvent.StartDate.NextYear, calendarEvent.EndDate.NextYear, calendarEvent.StartTime, calendarEvent.EndTime) {
        this.RegisterEventAction(calendarEvent.OnStart, calendarEvent.OnEnd);
    }

    public AnnualCalendarEvent(int month, int day) : base(new Date(month, day, Today.Instance.Date.YEAR /*This might be a problem*/), Time.Midnight, Time.EndOfDay) {
        //event automatically adds itself to the calendar for next year when this year's event ends
        this.onEnd += () => {
            CalendarEventController.Instance.AddEvent(new AnnualCalendarEvent(this));
        };
    }
    
}
