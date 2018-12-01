using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateComparer : IEqualityComparer<Date> {
    public bool Equals(Date a, Date b) {
        return (a.YEAR == b.YEAR) && (a.MONTH == b.MONTH) && (a.DAY == b.DAY);
    }

    public int GetHashCode(Date x) {
        return x.YEAR.GetHashCode() + x.MONTH.GetHashCode() + x.DAY.GetHashCode();
    }
}
