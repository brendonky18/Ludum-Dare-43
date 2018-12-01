using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarEvent {
    private Date startDate;
    private Date endDate;

    private Time startTime;
    private Time endTime;

    private Action onStart;
    private Action onEnd;

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

    private string eventName = "event";
    private bool isHidden = false;

    public CalendarEvent(Date startDate, Date endDate, Time startTime, Time endTime) {
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.startTime = startTime;
        this.endTime = endTime;
    }

    public CalendarEvent(Date date, Time startTime, Time endTime) : this(date, date, startTime, endTime) { }

    public CalendarEvent RegisterEventAction(Action start, Action end) {
        onStart = start == null ? (() => { Debug.Log(eventName + " started"); }) : start;
        onEnd = end == null ? (() => { Debug.Log(eventName + " ended"); }) : end;

        return this;
    }

    public CalendarEvent SetEventName(string eventName) {
        this.eventName = eventName;
        return this;
    }

    public CalendarEvent SetIsHidden(bool hideEvent) {
        isHidden = hideEvent;
        return this;
    }
}
