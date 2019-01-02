using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuzstate<T> : State<T>
{
    protected Fuzzy brain;
    protected Vector3 moveTarget;
    protected Vector3 moveDirection;
    protected Vector3 moveRotation;
    protected Quaternion lookAtRotation;
    private float gravityFactor = -50.0f;
    private float gravityForce = 0f;
    protected Transform target;
    private float angleToTurn;

    public fuzstate(T stateName, Fuzzy brain, float minDuration) : base(stateName, brain, minDuration)
    {
        this.brain = brain;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }

    public override void OnStateTriggerEnter(Collider collider)
    {
    }

    public override void Act()
    {
        if (!triggerEntered)
        {

            //calculate the rotation that is needed to face the waypoint
            moveRotation = Quaternion.Slerp(brain.transform.rotation, lookAtRotation, brain.turnSpeed * Time.deltaTime).eulerAngles;
            
            moveRotation.x = 0;
            moveRotation.z = 0;
            // Apply rotation to the character
            brain.gameObject.transform.rotation = Quaternion.Euler(moveRotation);
            angleToTurn = lookAtRotation.eulerAngles.y - brain.gameObject.transform.rotation.eulerAngles.y;
            // Move towards the target 
            if (Mathf.Abs(angleToTurn) < 1.0f)
            {
                moveDirection = (target.position - brain.gameObject.transform.position).normalized;
                // Set animation parameters
                brain.animator.SetFloat("Rotation", 0f);
                brain.animator.SetFloat("Speed", brain.forwardSpeed);
                brain.characterController.Move(ApplyGravity(moveDirection) * brain.forwardSpeed * Time.deltaTime);
            }
            else
            {
                // Set animation
                if (angleToTurn < 0f)
                {
                    brain.animator.SetFloat("Rotation", -brain.turnSpeed);
                }
                else
                {
                    brain.animator.SetFloat("Rotation", brain.turnSpeed);
                }
                brain.animator.SetFloat("Speed", 0f);
            }
        }
    }

    public override void Monitor()
    {

    }

   protected void TurnToFace() {
        // calculate the rotation to face the target
        Vector3 lookRotation = Quaternion.LookRotation(target.transform.position - brain.gameObject.transform.position).eulerAngles;
        lookRotation.x = 0;
        lookRotation.z = 0;
        // convert back to quaterion 
        lookAtRotation = Quaternion.Euler(lookRotation);
    }

    protected Vector3 ApplyGravity(Vector3 directionToMove)
    {
        if (brain.characterController.isGrounded)
        {
            gravityForce = 0.0f;
        }
        else
        {

            gravityForce = gravityFactor * Time.deltaTime;
        }
        // Apply gravity if character controller is not grounded
        directionToMove.y += gravityForce;
        return directionToMove;
    }





}
