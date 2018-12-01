using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time {
    //24 hr time
    private int hour;
    private int minute;
    private int second;

    private bool isPM;

    public Time TwelveHourTime {
        get {
            if (hour == 0)
                return new Time(12, minute, second, false);
            else if(hour >= 12)
                return new Time(hour - 12, minute, second, true);
            else
                return new Time(hour, minute, second, false);
        }
    }

    public Time(int hour, int minute, int second, bool isPM) {
        this.hour = hour;
        this.minute = minute;
        this.second = second;
        this.isPM = isPM;
    }

    public Time(int hour, int minute) : this(hour, minute, 0, hour >= 12) { }
    public Time(int hour) : this(hour, 0, 0, hour >= 12) { }
	
}
