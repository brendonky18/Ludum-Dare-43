using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyModifier {

    //what should be done every day
    private Action onTick;
    //when should this happen
    private Time modifierTick;
    
    public DailyModifier(Action onTick, Time modifierTick) {
        this.onTick = onTick;
        this.modifierTick = modifierTick;
    }

    public DailyModifier(Action onTick) : this(onTick, Time.Midnight) { }
}
