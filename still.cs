using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class still<T> : fuzstate<T>
    {
        public still(T stateName, Fuzzy controller, float minDuration) : base(stateName, controller, minDuration) { }


        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnLeave()
        {
            base.OnLeave();
        }

        public override void Act()
        {

        }
    }

