﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time {
    //24 hr time
    private int hour;
    private int minute;
    private int second;
    public int Hour {
        get {
            return hour;
        }

        protected set {
            hour = value;
        }
    }
    public int Minute {
        get {
            return minute;
        }

        protected set {
            minute = value;
        }
    }
    public int Second {
        get {
            return second;
        }

        protected set {
            second = value;
        }
    }

    public bool isPM;

    public Time TwelveHourTime {
        get {
            if (Hour == 0)
                return new Time(12, Minute, Second, false);
            else if(hour == 12)
                return new Time(12, Minute, Second, true);
            else if(Hour > 12)
                return new Time(Hour - 12, Minute, Second, true);
            else
                return new Time(Hour, Minute, Second, false);
        }
    }

    //constructors
    public Time(int hour, int minute, int second, bool isPM) {
        this.Hour = hour;
        this.Minute = minute;
        this.Second = second;
        this.isPM = isPM;
    }
    public Time(int hour, int minute, int second) {
        this.Hour = hour;
        this.Minute = minute;
        this.Second = second;
        this.isPM = hour >= 12;
    }
    public Time(int hour, int minute) : this(hour, minute, 0) { }
    public Time(int hour) : this(hour, 0, 0) { }
	
    //basic units of time
    public static Time Midnight {
        get {
            return new Time(0);
        }
    }
    public static Time Noon {
        get {
            return new Time(12);
        }
    }
    public static Time EndOfDay {
        get {
            return new Time(23, 59, 59);
        }
    }

    public Time RandomTime {
        get {
            return new Time(
                UnityEngine.Random.Range(0, 24)    
            );
        }
    }

    
    //returns true if the day should be changed
    public bool IncrementTimeBy(Time deltaTime) {
        int newSecond = second + deltaTime.Second;
        int newMinute = minute + deltaTime.Minute + newSecond / 60;
        int newHour = hour + deltaTime.Hour + newMinute / 60;

        second = newSecond % 60;
        minute = newMinute % 60;
        hour = newHour % 24;

        //if there's more than 24 hours, then the day should increment
        return newHour / 24 > 0;
    }
    //same as previous method, but if the day progresses then it will update everything
    public bool IncrementTimeBy(Time deltaTime, Action onDayChange) {
        int newSecond = second + deltaTime.Second;
        int newMinute = minute + deltaTime.Minute + newSecond / 60;
        int newHour = hour + deltaTime.Hour + newMinute / 60;

        second = newSecond % 60;
        minute = newMinute % 60;
        hour = newHour % 24;
        
        //if there's more than 24 hours, then the day should increment
        if(newHour > 24) {
            onDayChange();
            return true;
        } else {
            return false;
        }
    }
    public void IncrementTimyBySeconds(int deltaS) {
        int newSecond = second + deltaS;
        int newMinute = minute + newSecond / 60;
        int newHour = hour + newMinute / 60;

        second = newSecond % 60;
        minute = newMinute % 60;
        hour = newHour % 24;
    }
    public bool IncrementTimeBySeconds(int deltaS, Action changeToTomorrow) {
        int newSecond = second + deltaS;
        int newMinute = minute + newSecond / 60;
        int newHour = hour + newMinute / 60;

        second = newSecond % 60;
        minute = newMinute % 60;
        hour = newHour % 24;
        
        //if there's more than 24 hours, then the day should increment
        if (newHour >= 24) {
            changeToTomorrow();
            return true;
        } else {
            return false;
        }
    }
    public void IncrementTimyByMinutes(int deltaM) {
        int newMinute = minute + deltaM;
        int newHour = hour + newMinute / 60;

        minute = newMinute % 60;
        hour = newHour % 24;
    }
    public bool IncrementTimeByMinutes(int deltaM, Action changeToTomorrow) {
        int newMinute = minute + deltaM;
        int newHour = hour + newMinute / 60;

        minute = newMinute % 60;
        hour = newHour % 24;

        //if there's more than 24 hours, then the day should increment
        if (newHour >= 24) {
            changeToTomorrow();
            return true;
        } else {
            return false;
        }
    }
    public void IncrementTimeByHours(int deltaH) {
        int newHour = hour + deltaH;
        
        hour = newHour % 24;
    }
    public bool IncrementTimeByHours(int deltaH, Action changeToTomorrow) {
        int newHour = hour + deltaH;

        hour = newHour % 24;

        //if there's more than 24 hours, then the day should increment
        if (newHour >= 24) {
            changeToTomorrow();
            return true;
        } else {
            return false;
        }
    }

    public string ToString() {
        return hour + ":" + (minute / 10) + (minute % 10);// + ":" + (second / 10) + (second % 10);
    }

    public bool IsGreaterThan(Time t) {
        //I COULD make this a single boolean statement but it makes more sense this way
        if (Hour > t.Hour)
            return true;
        else if (Minute > t.Minute) {
            return true;
        } else {
            return Second >= t.Second;
        }
    }

    public override bool Equals(object obj) {
        var other = obj as Time;

        if (other == null)
            return false;
        else
            return (this.Hour == other.Hour) && (this.Minute == other.Minute) && (this.Second == other.Second);
    }

    public override int GetHashCode() {
        return this.Hour.GetHashCode() + this.Minute.GetHashCode() + this.Second.GetHashCode();
    }
}
