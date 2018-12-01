using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnualCalendarEvent : CalendarEvent {

    public AnnualCalendarEvent(int month, int day) : base(new Date(month, day, 0), Time.Midnight, Time.EndOfDay) { }
}
