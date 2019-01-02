using UnityEngine;
using System.Collections;

public class AIState<T> : State<T> {
    protected StateDrivenBrain brain;
    protected Vector3 moveTarget;
    protected Vector3 moveDirection;
    protected Vector3 moveRotation;
    protected Quaternion lookAtRotation;
    private float gravityFactor = -50.0f;
    private float gravityForce = 0f;



    public AIState(T stateName, StateDrivenBrain brain, float minDuration): base(stateName, brain, minDuration) {
        this.brain = brain;
    }

    public override void OnEnter() {
        base.OnEnter();
    }

    public override void OnLeave() {
        base.OnLeave();
    }

    public override void OnStateTriggerEnter(Collider collider) {
    }

    public override void Act() {
    }

    public override void Monitor() {

    }

    protected Quaternion TurnToFace(Vector3 target) {
        Vector3 lookRotation = Quaternion.LookRotation(target - brain.gameObject.transform.position).eulerAngles;
        lookRotation.x = 0;
        lookRotation.z = 0;
        return Quaternion.Euler(lookRotation);
    }

    protected Vector3 ApplyGravity(Vector3 directionToMove)
    {
        if (brain.characterController.isGrounded) {
            gravityForce = 0.0f;
        }
        else {

            gravityForce = gravityFactor * Time.deltaTime;
        }
        // Apply gravity
        directionToMove.y += gravityForce;
        return directionToMove;
    }





}
