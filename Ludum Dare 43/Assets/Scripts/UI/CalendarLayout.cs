using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarLayout : MonoBehaviour {
    public readonly int CALENDAR_SIZE = 42;

    public GameObject calendarFrame, calendarBody, calendarHeaderText, calendarHeader;

    public GameObject calendarDateFramePrefab;
    private GameObject[] calendarDateFrames;

    public GameObject eventFlagPrefab;

    private GridLayoutGroup gridLayoutGroup;
    private RectTransform rectTransform;

    void Awake() {
        calendarDateFrames = new GameObject[CALENDAR_SIZE];

        for (int i = 0; i < CALENDAR_SIZE; i++) {
            calendarDateFrames[i] = Instantiate(calendarDateFramePrefab, calendarBody.transform);
            calendarDateFrames[i].name = "Calendar Date Frame " + i;
        }
    }

    private void Start() {
        //refresh the UI
        calendarBody.GetComponent<LayoutElement>().ignoreLayout = true;
        calendarBody.GetComponent<LayoutElement>().ignoreLayout = false;


        gridLayoutGroup = calendarBody.GetComponent<GridLayoutGroup>();
        rectTransform = calendarBody.GetComponent<RectTransform>();

        float calendarBodyHeight = Screen.height * (49f / 57f);
        float calendarBodyWidth = Screen.height;

        gridLayoutGroup.cellSize = new Vector2(calendarBodyWidth / 7f, calendarBodyWidth / 7f);
    }

    void Update() {
        float totalTime = 0.25f;
        Vector3 displacement = Vector3.Lerp(Vector3.zero, verticalOffset, UnityEngine.Time.deltaTime / totalTime);
        if(UnityEngine.Time.time <= startTime + totalTime) {
            calendarFrame.GetComponent<RectTransform>().position += displacement;
        }
    }

    public void DisplayDate(Date date) {
        Day startDay = Date.DayOfWeek(date);

        calendarHeaderText.GetComponent<Text>().text = Date.MonthString((Month)date.MONTH) + " " + date.YEAR;

        //sets up date frames
        for(int i = 0; i < (int) startDay; i++) {
            calendarDateFrames[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>().text = "";
        }

        for (int i = 0; i < Date.MonthDays(date); i++) {
            calendarDateFrames[i + (int)startDay].transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>().text =
                Date.DayString((Day)((i + (int)startDay) % 7)) + "\n" + (i + 1);

            GameObject instantiatedEventFlag;
            foreach (CalendarEvent upcomingEvent in World.Instance.GetEventsStartingOnDay(new Date(date.YEAR, date.MONTH, i + 1))) {
                if (upcomingEvent.IsHidden == false) {
                    Debug.Log("instantiating event flag");
                    instantiatedEventFlag = Instantiate(eventFlagPrefab, calendarDateFrames[i + (int)startDay].transform.GetChild(0));
                    instantiatedEventFlag.GetComponent<Text>().text = upcomingEvent.ToString();
                }
            }
        }

        for (int i = Date.MonthDays(date) + (int)startDay; i < CALENDAR_SIZE; i++) {
            calendarDateFrames[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>().text = "";
        }
    }

    private bool isDocked = false;
    private float startTime;
    private Vector3 startPosition;
    Vector3 verticalOffset;
    public void DockCalendar() {
        startTime = UnityEngine.Time.time;
        if (isDocked)
            verticalOffset = new Vector3(0f, Screen.height - calendarHeader.GetComponent<RectTransform>().rect.height, 0f);
        else
            verticalOffset = new Vector3(0f, calendarHeader.GetComponent<RectTransform>().rect.height - Screen.height, 0f);

        isDocked = !isDocked;
    }
}
