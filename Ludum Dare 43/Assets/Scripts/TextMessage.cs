using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMessage {
    private static Dictionary<string, TextMessage> messages = new Dictionary<string, TextMessage> {
        { "Test Message", new TextMessage(
            new List<string> {
                "Hello world",
                "Testing, Testing, 123",
                "This is a test",
                "Hey, is this thing working"
            },
            new string[3] {
                "yes",
                "indubitably",
                "aww, yuss"
            },
            new Dictionary<string, Action>() {
                {
                    "yes", () => { Debug.Log("yes selected"); }
                },
                {
                    "indubitably",
                    () => {
                        Debug.Log("indubitably selected");
                    }
                },
                {
                    "aww, yuss",
                    () => {
                        Debug.Log("aww, yuss selected");
                    }
                }
            }
        )}
    };
    public static Dictionary<string, TextMessage> Messages {
        get {
            return messages;
        }

        set {
            messages = value;
        }
    }

    private List<string> allMessages;

    private string[] replies;
    public string[] Replies {
        get {
            return replies;
        }

        set {
            replies = value;
        }
    }

    private Dictionary<string, Action> onReply;
    public Dictionary<string, Action> OnReply {
        get {
            return onReply;
        }

        set {
            onReply = value;
        }
    }

    

    public TextMessage(List<string> allMessages, string[] replies, Dictionary<string, Action> onReply) {
        this.allMessages = allMessages;
        this.replies = replies;
        this.OnReply = onReply;
    }

    public string RandomMessage() {
        return allMessages[UnityEngine.Random.Range(0, allMessages.Count)];
    }
}
