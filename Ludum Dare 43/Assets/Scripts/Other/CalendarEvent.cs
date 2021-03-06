﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarEvent {
    private Date startDate;
    private Date endDate;
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

    private Time startTime;
    private Time endTime;
    public Time StartTime {
        get {
            return startTime;
        }

        protected set {
            startTime = value;
        }
    }
    public Time EndTime {
        get {
            return endTime;
        }

        protected set {
            endTime = value;
        }
    }

    private Action onStart;
    protected Action onEnd;
    public Action OnStart {
        get { return onStart; }
    }
    public Action OnEnd {
        get { return onEnd; }
    }
    public void StartEvent() {
        Debug.Log(eventName + " has started");
        onStart();
    }
    public void EndEvent() {
        onEnd();
    }
    

    private string eventName = "event";
    private bool isHidden = false;
    public bool IsHidden {
        get { return isHidden; }
    }

    public CalendarEvent() {

    }

    public CalendarEvent(Date startDate, Date endDate, Time startTime, Time endTime) {
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.StartTime = startTime;
        this.EndTime = endTime;
    }
    public CalendarEvent(Date date, Time startTime, Time endTime) : this(date, date, startTime, endTime) { }
    public CalendarEvent(Date date) : this(date, date, Time.Midnight, Time.EndOfDay) { }

    public CalendarEvent RegisterEventAction(Action start, Action end) {
        onStart += start == null ? (() => { Debug.Log(eventName + " started"); }) : start;
        onEnd += end == null ? (() => { Debug.Log(eventName + " ended"); }) : end;

        return this;
    }
    public CalendarEvent RegisterEventAction() {
        return RegisterEventAction(null, null);
    }
    public CalendarEvent SetEventName(string eventName) {
        this.eventName = eventName;
        return this;
    }
    public CalendarEvent SetIsHidden(bool hideEvent) {
        isHidden = hideEvent;
        return this;
    }
    public CalendarEvent SetDate(Date date) {
        this.startDate = date;
        this.endDate = date;
        return this;
    }
    public CalendarEvent SetTime(Time time) {
        this.StartTime = time;
        this.EndTime = time;

        return this;
    }
    public CalendarEvent SetTime(Time startTime, Time endTime) {
        this.StartTime = startTime;
        this.EndTime = endTime;
        return this;
    }


    public string ToString() {

        string returnString = "";
        //all day
        if (isHidden) { //hidden event
            returnString = "(hidden event) " + eventName;
        } else if (startTime.Equals(Time.Midnight) && endTime.Equals(Time.EndOfDay)) { //all day
            returnString = eventName;
        } else if( startTime == endTime) { //single event
            returnString = eventName;
        } else {
            returnString = "(" + startTime.ToString() + ") " + eventName;
        }
        return returnString;
    }
}
