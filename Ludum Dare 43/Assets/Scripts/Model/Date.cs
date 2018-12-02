using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Date {
    public Date(int year, int month, int day, bool isLeapYear){
        YEAR = year;
        MONTH = month;
        DAY = day;
        IS_LEAP_YEAR = isLeapYear;
    }
    //if leap year is not specified, it is assumed to be a leap year when divisible by 4
    public Date(int year, int month, int day) : this(year, month, day, year % 4 == 0) { }
    public Date(int year, int month) : this(year, month, 1) { }
    private static Dictionary<Month, int> monthCode = new Dictionary<Month, int> {
        {Month.Jan, 6 },
        {Month.Feb, 2 },
        {Month.Mar, 2 },
        {Month.Apr, 5 },
        {Month.May, 0 },
        {Month.Jun, 3 },
        {Month.Jul, 5 },
        {Month.Aug, 1 },
        {Month.Sep, 4 },
        {Month.Oct, 6 },
        {Month.Nov, 2 },
        {Month.Dec, 4 },
    };
    private static Dictionary<Month, int> leapYearMonthCode = new Dictionary<Month, int> {
        {Month.Jan, 5 },
        {Month.Feb, 1 },
        {Month.Mar, 2 },
        {Month.Apr, 5 },
        {Month.May, 0 },
        {Month.Jun, 3 },
        {Month.Jul, 5 },
        {Month.Aug, 1 },
        {Month.Sep, 4 },
        {Month.Oct, 6 },
        {Month.Nov, 2 },
        {Month.Dec, 4 },
    };

    private static Dictionary<int, int> LeapYearCode = new Dictionary<int, int> {
        {2000, 0 },
        {2004, 5 },
        {2008, 3 },
        {2012, 1 },
        {2016, 6 },
        {2020, 4 },
        {2024, 2 },
        {2028, 0 },
        {2032, 5 },
        {2036, 3 },
        {2040, 1 },
        {2044, 6 },
        {2048, 4 },
        {2052, 2 },
    };

    private static Dictionary<Month, int> monthDays = new Dictionary<Month, int> {
        {Month.Jan, 31 },
        {Month.Feb, 28 },
        {Month.Mar, 31 },
        {Month.Apr, 30 },
        {Month.May, 31 },
        {Month.Jun, 30 },
        {Month.Jul, 31 },
        {Month.Aug, 31 },
        {Month.Sep, 30 },
        {Month.Oct, 31 },
        {Month.Nov, 30 },
        {Month.Dec, 31 },
    };

    //returns the month code used for calculating the day of the week
    public static int MonthCode(Month month, bool isLeapYear) {
        if (isLeapYear) {
            return leapYearMonthCode[month];
        } else {
            return monthCode[month];
        }
    }
    //year code for calculating day of the week
    public static int YearCode(int year) {
        if (year < 2000 || year > 2052) {
            Debug.LogError("Year asked for is out of range");
        }
        //                         *  Integer Division to get the previous leap year
        return (LeapYearCode[(year / 4) * 4] + year % 4) % 7;
    }

    public static int MonthDays(Month month, bool isLeapYear) {
        //if is a leap year and is looking for feb, thenreturn 29, else look up in dictionary
        return isLeapYear && month == Month.Feb ? 29 : monthDays[month];
    }
    public static int MonthDays(Month month) {
        //if is a leap year and is looking for feb, thenreturn 29, else look up in dictionary
        return monthDays[month];
    }
    public static int MonthDays(Date date) {
        //if is a leap year and is looking for feb, thenreturn 29, else look up in dictionary
        return date.YEAR % 4 == 0 && (Month)date.MONTH == Month.Feb ? 29 : monthDays[(Month)date.MONTH]; //shouldn't have to deal with the fact that every 100 years isn't a leap year but 500 years is a leap year
    }

    //formula based off of this: http://gmmentalgym.blogspot.com/2011/03/day-of-week-for-any-date-revised.html#ndatebasics
    public static Day DayOfWeek(Date date) {
        return (Day) ((YearCode(date.YEAR) + MonthCode((Month)date.MONTH, date.IS_LEAP_YEAR) + 1) % 7);
    }

    public static string DayString(Day day) {
        switch (day) {
            case Day.Sun:
                return "Sun";
            case Day.Mon:
                return "Mon";
            case Day.Tue:
                return "Tue";
            case Day.Wed:
                return "Wed";
            case Day.Thu:
                return "Thu";
            case Day.Fri:
                return "Fri";
            case Day.Sat:
                return "Sat";
            default:
                return "Something went wrong here";
        }
    }
    public static string MonthString(Month month) {
        switch (month) {
            case Month.Jan:
                return "Jan";
            case Month.Feb:
                return "Feb";
            case Month.Mar:
                return "Mar";
            case Month.Apr:
                return "Apr";
            case Month.May:
                return "May";
            case Month.Jun:
                return "Jun";
            case Month.Jul:
                return "Jul";
            case Month.Aug:
                return "Aug";
            case Month.Sep:
                return "Sep";
            case Month.Oct:
                return "Oct";
            case Month.Nov:
                return "Nov";
            case Month.Dec:
                return "Dec";
            default:
                return "Something went wrong";
        }
    }

    public readonly bool IS_LEAP_YEAR;

    public readonly int YEAR;
    public readonly int MONTH;
    public readonly int DAY;


    public static Date RandomThisYear {
        get {
            int randomMonth = Random.Range(World.Instance.Date.MONTH, 13);

            return new Date(
                World.Instance.Date.YEAR,
                (int)randomMonth,
                Random.Range(1, MonthDays(new Date(World.Instance.Date.YEAR, randomMonth, 1)) + 1)
            );
        }
    }
    public static Date RandomThisMonth {
        get {

            return new Date(
                World.Instance.Date.YEAR,
                World.Instance.Date.MONTH,
                Random.Range(World.Instance.Date.DAY, MonthDays(World.Instance.Date) + 1)
            );
        }
    }

    //TODO: probably doesn't work for leap years, we'll check that out eventually
    public Date Yesterday {
        get {
            int newDay;
            int newMonth;
            int newYear;

            if(DAY == 1) {
                if (MONTH == 1) {
                    newMonth = 1;
                    newYear = YEAR - 1;
                } else {
                    newMonth = MONTH - 1;
                    newYear = YEAR;
                }
                newDay = Date.MonthDays(new Date(newYear, newMonth));
            } else {
                newDay = DAY - 1;
                newMonth = MONTH;
                newYear = YEAR;
            }
            return new Date(newYear, newMonth, newDay);

        }
    }
    public Date Tomorrow {
        get {
            int newDay;
            int newMonth;
            int newYear;

            if (DAY == Date.MonthDays(new Date(YEAR, MONTH))) {
                if (MONTH == 12) {
                    newMonth = 1;
                    newYear = YEAR + 1;
                } else {
                    newMonth = MONTH + 1;
                    newYear = YEAR;
                }
                newDay = 1;
            } else {
                newDay = DAY + 1;
                newMonth = MONTH;
                newYear = YEAR;
            }
            return new Date(newYear, newMonth, newDay);
        }
    }

    public Date NextYear {
        get {
            return new Date(YEAR + 1, MONTH, DAY);
        }
    }
    public string ToString() {
        return YEAR + "-" + MONTH + "-" + DAY;
    }



    //so the comparison stuff works

    public override bool Equals(object obj) {
        var other = obj as Date;

        if (other == null)
            return false;
        else
            return (this.YEAR == other.YEAR) && (this.MONTH == other.MONTH) && (this.DAY == other.DAY);
    }

    public override int GetHashCode() {
        return this.YEAR.GetHashCode() + this.MONTH.GetHashCode() + this.DAY.GetHashCode();
    }
}
