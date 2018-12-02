using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public GameObject calendarFrame;
    private bool calendarIsDocked = false;
	public void DockCalendar() {
        Debug.Log("DockCalendar()");
        Vector3 verticalOffset = new Vector3(0, calendarFrame.transform.GetChild(1).gameObject.GetComponent<RectTransform>().rect.height, 0);

        if (calendarIsDocked) {
            calendarFrame.GetComponent<RectTransform>().localPosition += verticalOffset;
        } else {
            calendarFrame.GetComponent<RectTransform>().localPosition -= verticalOffset;
        }
        calendarIsDocked = !calendarIsDocked;

    }
}
