using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FindPlayer<T> : AIState<T>
{
    public FindPlayer(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }


    public override void OnEnter()
    {
        base.OnEnter();
        if (brain.senses.target)
        {
            
            brain.nav.destination = brain.senses.target.transform.position; // give the nav mesh destination the position of the target
            brain.speed = brain.walkSpeed; // set the speed of the object 
            brain.nav.speed = brain.walkSpeed; //  set the nav meshes speed
            brain.animator.SetFloat("Speed", brain.speed); // set the naimators speed 
            if(brain.transform.position.z == brain.senses.target.transform.position.z)
            { // if the object reaches its target position then set speed of animator back to 0
                brain.animator.SetFloat("Speed", 0f);
            }
        }

    }

    public override void OnLeave()
    {
        base.OnLeave();
    }

    public override void Act()
    {
        
    }
}
