using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
    //all the stats that I could think of
    private Dictionary<string, float> playerStats = new Dictionary<string, float> {
        {"Grades", 0.5f },
        {"Money", 1000f },
        {"Friends", 10f },
        {"Family", 0.5f },
        {"Social Life", 0.5f },
        {"Health", 0.9f },
        {"Stress", 0.0f },
        {"Happiness", 0.5f },
        {"Debt", 0f },
    };
}
