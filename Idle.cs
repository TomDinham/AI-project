﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle<T> : AIState<T>
{
    public Idle(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }


    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("IDLE"); 
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }

    public override void Act()
    {

    }
}
