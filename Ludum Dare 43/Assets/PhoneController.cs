using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneController : MonoBehaviour {
    public Sprite[] phoneSprites = new Sprite[3];
    private Image phoneImage;

    private PhoneScreen Screen {
        set { phoneImage.sprite = phoneSprites[(int)value]; }
    }

    [SerializeField] private List<GameObject> replyButtons;
    [SerializeField] private List<GameObject> getScreen;

    //GameObject references
    public GameObject senderText, messageText, player;

    private void Awake() {
        phoneImage = gameObject.GetComponent<Image>();
    }
    // Use this for initialization
    void Start () {
        SwitchToScreen(PhoneScreen.Locked);
    }

    public void SwitchToScreen(PhoneScreen setScreen) {
        //sets the correct sprite
        Screen = setScreen;
        //deactivates all the other screens
        for (PhoneScreen i = 0; i <= PhoneScreen.Clock; i++) {
            getScreen[(int)i].SetActive(i == setScreen);
        }
    }

    public void LockScreen() {
        SwitchToScreen(PhoneScreen.Locked);
    }

    public void UnlockScreen() {
        SwitchToScreen(PhoneScreen.Clock);
    }

    public void ReceiveMessage(string sender, TextMessage message) {
        SwitchToScreen(PhoneScreen.Message);
        
        //TODO: implement better system to get names
        senderText.GetComponent<Text>().text = sender;

        messageText.GetComponent<Text>().text = message.RandomMessage();

        //set up the reply buttons
        GameObject curButton;
        string curReply;
        for (int i = 0; i < message.Replies.Length; i++) {
            curButton = replyButtons[i];
            curReply = message.Replies[i];

            curButton.SetActive(true);

            curButton.GetComponentInChildren<Text>().text = curReply;

            curButton.GetComponent<ReplyButtonController>().OnReply = message.OnReply[curReply];
        }
    }

    /*
     * All possible messages (pick from randomized list)
     * All responses
     *  What those responses entail, what gets triggered (applies modifiers, or changes something)
     */
}
