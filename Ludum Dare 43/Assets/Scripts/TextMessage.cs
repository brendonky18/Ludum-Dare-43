using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMessage {
    private static Dictionary<string, TextMessage> messages = new Dictionary<string, TextMessage> {
        { "Test Message", new TextMessage(
            "Totally not a bot",
            new List<string> {
                "Hello world",
                "Testing, Testing, 123",
                "This is a test",
                "Hey, is this thing working"
            },
            new List<string> {
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

    private List<string> replies;
    public List<string> Replies {
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

    private string sender;
    public string Sender {
        get { return sender; }
        set { sender = value; }
    }
    

    public TextMessage(string sender, List<string> allMessages, List<string> replies, Dictionary<string, Action> onReply) {
        this.sender = sender;
        this.allMessages = allMessages;
        this.replies = replies;
        this.OnReply = onReply;
    }

    public TextMessage SetAllMessages(List<string> messages) {
        allMessages = messages;
        return this;
    }
    public TextMessage AddMessage(string message) {
        if(allMessages == null) {
            allMessages = new List<string>();
        }

        allMessages.Add(message);

        return this;
    }
    public TextMessage SetAllReplies(List<string> replies) {
        this.replies = replies;
        return this;
    }
    public TextMessage AddReply(string reply) {
        if (replies == null)
            replies = new List<string>();

        replies.Add(reply);

        return this;
    }
    public TextMessage SetAllOnReply(Dictionary<string, Action> onReply) {
        this.onReply = onReply;
        return this;
    }
    public TextMessage AddOnReply(KeyValuePair<string, Action> onReply) {
        AddOnReply(onReply.Key, onReply.Value);
        return this;
    }
    public TextMessage AddOnReply (string key, Action value) {
        onReply.Add(key, value);
        return this;
    }
    public TextMessage SetSender(string sender) {
        this.sender = sender;
        return this;
    }

    public bool IsValid() {
        return
            allMessages != null && allMessages.Count > 0 &&
            replies != null && replies.Count > 0 &&
            onReply != null && onReply.Count > 0 &&
            sender != null;
    }
    public string RandomMessage() {
        return allMessages[UnityEngine.Random.Range(0, allMessages.Count)];
    }

    private string message;
    public string Message {
        get {
            if (message == null)
                message = RandomMessage();
            return message;
        }
    }
}
