using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatModifier : Modifier {
    private bool hasNotBeenApplied;

    public FlatModifier(string name, float value, Stats statModified, Date expirationDate) : base(name, value, statModified, expirationDate) {
        hasNotBeenApplied = false;
    }

    public override void ApplyModifier(Action<Stats, float> ModifyStat) {
        if (hasNotBeenApplied) {
            ModifyStat(statModified, value);
        }
    }
}
