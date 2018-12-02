using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplyButtonController : MonoBehaviour {
    private Action onReply;
    public Action OnReply {
        get {
            return onReply;
        }

        set {
            onReply = value;
        }
    }

    public void Clicked() {
        if (onReply != null)
            onReply();
        else
            Debug.LogError("Reply action is null");
    }

    private void OnDisable() {
        onReply = null;
    }
}
