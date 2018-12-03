using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedModifier : Modifier {
    private Time modifierTick;
    
    public TimedModifier(string name, float value, Stats statModified, Date expirationDate, Time modifierTick) : base(name, value, statModified, expirationDate) {
        this.modifierTick = modifierTick;
    }

    public TimedModifier(string name, float value, Stats statModified, Date expirationDate) : this(name, 1f, statModified, expirationDate, Time.Midnight) { }

    public override void ApplyModifier(Action<Stats, float> ModifyStat) {
            ModifyStat(statModified, value);
    }
}
