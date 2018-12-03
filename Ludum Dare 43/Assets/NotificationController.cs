using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationController : MonoBehaviour {
    private Action onOpen;
    public Action OnOpen{
        get {
            return onOpen;
        }

        set {
            onOpen = value;
        }
    }

    public void Clicked() {
        if (onOpen != null)
            onOpen();
        else
            Debug.LogError("Reply action is null");
        gameObject.SetActive(false);
    }
}
