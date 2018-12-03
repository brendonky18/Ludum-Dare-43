using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneController : MonoBehaviour {
    private Queue<Notification> pendingNotifications = new Queue<Notification>();

    public Sprite[] phoneSprites = new Sprite[3];
    private Image phoneImage;

    private PhoneScreen curScreen = PhoneScreen.Locked;

    private PhoneScreen Screen {
        set {
            curScreen = value;
            phoneImage.sprite = phoneSprites[(int)curScreen];
        }
    }

    [SerializeField] private List<GameObject> replyButtons;
    [SerializeField] private List<GameObject> getScreen;

    //GameObject references
    public GameObject senderText, messageText, player, notificationContainer, notificationSenderText, notificationText;

    private void Awake() {
        phoneImage = gameObject.GetComponent<Image>();
    }
    // Use this for initialization
    void Start() {
        //Debug.Log("Start()");
        //SwitchToScreen(PhoneScreen.Locked);
    }

    public void SwitchToScreen(PhoneScreen setScreen) {
        Debug.Log("Swicth to Screen: " + (int)setScreen);
        //sets the correct sprite
        Screen = setScreen;
        //deactivates all the other screens
        for (PhoneScreen i = 0; i < PhoneScreen.End; i++) {
            Debug.Log("screen: " + i + " " + (i == setScreen));
            getScreen[(int)i].SetActive(i == setScreen);
        }
    }

    public void LockScreen() {
        Debug.Log("Lock Screen");
        SwitchToScreen(PhoneScreen.Locked);
    }

    public void UnlockScreen() {
        Debug.Log("Unlock Screen");
        SwitchToScreen(PhoneScreen.Unlock);

        if(pendingNotifications.Count > 0) {
            Notification n = pendingNotifications.Dequeue();
            notificationContainer.SetActive(true);

            notificationContainer.GetComponent<NotificationController>().OnOpen = n.OnOpen;

            notificationSenderText.GetComponent<Text>().text = n.Sender;
            notificationText.GetComponent<Text>().text = (pendingNotifications.Count > 0 ? ((pendingNotifications.Count + 1) + " notifications") : n.Message);
        }
    }

    public void OpenMessage(TextMessage message) {
        SwitchToScreen(PhoneScreen.Message);

        //TODO: implement better system to get names
        senderText.GetComponent<Text>().text = message.Sender;

        messageText.GetComponent<Text>().text = message.Message;

        //set up the reply buttons
        GameObject curButton;
        string curReply;
        for (int i = 0; i < message.Replies.Count; i++) {
            curButton = replyButtons[i];
            curReply = message.Replies[i];

            curButton.SetActive(true);

            curButton.GetComponentInChildren<Text>().text = curReply;

            curButton.GetComponent<ReplyButtonController>().OnReply = message.OnReply[curReply];
        }
    }

    public void ReceiveNotification(Notification n) {
        Debug.Log("received notification");
        pendingNotifications.Enqueue(n);
        if (curScreen == PhoneScreen.Locked)
            UnlockScreen();
    }
    

    public void ReceiveTextNotification(TextMessage message) {
        Notification notification = new Notification(message.Sender, message.Message, () => { OpenMessage(message); });
        ReceiveNotification(notification);
    }


    

    /*
     * All possible messages (pick from randomized list)
     * All responses
     *  What those responses entail, what gets triggered (applies modifiers, or changes something)
     */
}
