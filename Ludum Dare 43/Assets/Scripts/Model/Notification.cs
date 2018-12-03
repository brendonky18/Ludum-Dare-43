using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour {
    private string sender;
    private string message;
    public string Sender {
        get {
            return sender;
        }

        set {
            sender = value;
        }
    }
    public string Message {
        get {
            return message;
        }

        set {
            message = value;
        }
    }

    private Action onOpen;
    public Action OnOpen {
        get {
            return onOpen;
        }

        set {
            onOpen = value;
        }
    }

    public Notification(string sender, string message, Action onOpen) {
        this.Sender = sender;
        this.Message = message;
        this.OnOpen = onOpen;
    }
}
