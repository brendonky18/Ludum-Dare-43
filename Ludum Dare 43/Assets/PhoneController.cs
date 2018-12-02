using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneController : MonoBehaviour {
    public Sprite[] phoneSprites = new Sprite[3];
    private SpriteRenderer phoneSR;

    private Phone Screen {
        set { phoneSR.sprite = phoneSprites[(int)value]; }
    }
    

    private void Awake() {
        phoneSR = gameObject.GetComponent<SpriteRenderer>();
    }
    // Use this for initialization
    void Start () {
        Screen = Phone.Locked;
	}


    public void ReceiveMessage(TextMessage message) {
        //display this message
        Screen = Phone.Message;
    }

    /*
     * All possible messages (pick from randomized list)
     * All responses
     *  What those responses entail, what gets triggered (applies modifiers, or changes something)
     */
}
