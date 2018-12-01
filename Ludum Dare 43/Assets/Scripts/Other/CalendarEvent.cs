using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarEvent {
    private Date startDate;
    private Date endDate;

    private Time startTime;
    private Time endTime;

    public Date StartDate {
        get {
            return startDate;
        }

        set {
            startDate = value;
        }
    }
    public Date EndDate {
        get {
            return endDate;
        }

        set {
            endDate = value;
        }
    }

    public CalendarEvent(Date startDate, Date endDate, Time startTime, Time endTime) {
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.startTime = startTime;
        this.endTime = endTime;
    }

    public CalendarEvent(Date date, Time startTime, Time endTime) : this(date, date, startTime, endTime) { }
}
