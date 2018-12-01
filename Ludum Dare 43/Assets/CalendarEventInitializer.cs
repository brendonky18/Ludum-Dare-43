using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarEventInitializer : MonoBehaviour {
    private CalendarEvent firstDayOfSchool = new CalendarEvent(Calendar.JUNIOR_YEAR_HS_START);
    void Start  () {
        firstDayOfSchool.SetEventName("First Day of School").RegisterEventAction();
        CalendarEventController.Instance.AddEvent(firstDayOfSchool);
    }
}
