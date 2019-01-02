using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flee<T> : fuzstate<T>
{
    public flee(T stateName, Fuzzy controller, float minDuration) : base(stateName, controller, minDuration) { }


    public override void OnEnter()
    {
        base.OnEnter();
        target = brain.fleePoint; // set target to the transform 
        TurnToFace(); //send to fuzstate turn to face
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }

    
}
