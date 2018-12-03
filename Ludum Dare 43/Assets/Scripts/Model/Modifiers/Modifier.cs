using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier : MonoBehaviour {
    protected string name;
    protected float value;
    protected Date expirationDate;
    public bool IsExpired { //exclusive, does not include date
        get {
            Date today = World.Instance.Date;
            return today.IsGreaterThanOrEqualTo(expirationDate);
        }
    }
    protected Action onExpire;

    protected Stats statModified;

    public Modifier(string name, float value, Stats statModified, Date expirationDate) {
        this.name = name;
        this.value = value;
        this.statModified = statModified;
        this.expirationDate = expirationDate;
        onExpire += () => { Debug.Log(name + " expired"); };
    }

    public Modifier(string name, float value, Stats statModified) : this(name, value, statModified, World.Instance.Date.Tomorrow){ }//one-day modifier

    public abstract void ApplyModifier(Action<Stats, float> ModifyStat);
}
