using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CougarController : MonoBehaviour
{
    public Transform[] PatrolRoute;
    private int currentWaypointIndex=0;
    public enum DriveStates {DriveForward, Stop, OutOfControl};
    public DriveStates drivestate;
    private CougarMover Mover;

   void Awake()
    {
        Mover = GetComponent<CougarMover>();
        drivestate = DriveStates.DriveForward;
        Mover.target = PatrolRoute[currentWaypointIndex]; // set the target to the transform in patrol routes 
    }
   void OnTriggerEnter(Collider other)
    {
        if(other.tag=="node")// if the colliders tag is equal to node
        {
            Debug.Log(PatrolRoute.Length);
            if (currentWaypointIndex + 1 == (PatrolRoute.Length))  // if the current waypoint index + 1 is equal to the patrol route length then set the index back to 0 and reset the target
            {
                currentWaypointIndex = 0;
                Mover.target = PatrolRoute[currentWaypointIndex]; 
             
            }
            else
            {
                Mover.target = PatrolRoute[++currentWaypointIndex];// else set the target to the next transform in patrol route
            }

        }
    }

}
