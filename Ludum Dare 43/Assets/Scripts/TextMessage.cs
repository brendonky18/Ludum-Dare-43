using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMessage {
    private static Dictionary<string, TextMessage> messages;


    private List<string> allMessages;

    private string[] responses;
    public string[] Responses {
        get {
            return responses;
        }

        set {
            responses = value;
        }
    }

    private Dictionary<string, Action> onReply;

    public TextMessage(List<string> allMessages, string[] responses, Dictionary<string, Action> onReply) {
        this.allMessages = allMessages;
        this.responses = responses;
        this.onReply = onReply;
    }

    public string ReceiveRandom() {
        return allMessages[UnityEngine.Random.Range(0, allMessages.Count)];
    }
}
