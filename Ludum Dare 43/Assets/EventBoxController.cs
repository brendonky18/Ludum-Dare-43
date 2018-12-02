using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBoxController : MonoBehaviour {
    private List<Action> options;
    public GameObject buttonContainer;

    public void RegisterOptions(List<Action> options) {
        this.options = options;
    }

    private void Awake() {
        
    }
}
