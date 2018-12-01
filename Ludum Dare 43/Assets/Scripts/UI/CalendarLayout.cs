using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarLayout : MonoBehaviour {
    public readonly int CALENDAR_SIZE = 42;

    public GameObject calendarBody;

    public GameObject calendarHeader;

    public GameObject calendarDateFramePrefab;
    private GameObject[] calendarDateFrames;

    private GridLayoutGroup gridLayoutGroup;
    private RectTransform rectTransform;

    void Awake() {
        calendarDateFrames = new GameObject[CALENDAR_SIZE];

        for (int i = 0; i < CALENDAR_SIZE; i++) {
            calendarDateFrames[i] = Instantiate(calendarDateFramePrefab, calendarBody.transform);
            calendarDateFrames[i].name = "" + i;
        }
    }

    void Update() {
        gridLayoutGroup = calendarBody.GetComponent<GridLayoutGroup>();   
        rectTransform = calendarBody.GetComponent<RectTransform>();

        gridLayoutGroup.cellSize = new Vector2(rectTransform.rect.width / 7f, rectTransform.rect.height / 6f);
    }

    public void DisplayDate(Date date) {
        Day startDay = Date.DayOfWeek(date);

        calendarHeader.GetComponentInChildren<Text>().text = Date.MonthString((Month)date.MONTH) +   + date.YEAR;

        //sets up date frames
        for(int i = 0; i < (int) startDay; i++) {
            calendarDateFrames[i].transform.GetChild(0).GetComponentInChildren<Text>().text = "";
        }

        for (int i = 0; i < Date.MonthDays(date); i++) {
            calendarDateFrames[i + (int)startDay].transform.GetChild(0).GetComponentInChildren<Text>().text =
                Date.DayString((Day)((i + (int)startDay) % 7)) + "\n" + (i + 1);
        }

        for (int i = Date.MonthDays(date) + (int)startDay; i < CALENDAR_SIZE; i++) {
            calendarDateFrames[i].transform.GetChild(0).GetComponentInChildren<Text>().text = "";
        }
    }
}
