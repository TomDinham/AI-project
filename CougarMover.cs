using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CougarMover : MonoBehaviour
{
    private WheelCollider[] Cougar;
    public WheelCollider fl;
    public WheelCollider fr;
    public WheelCollider bl;
    public WheelCollider br;
    private float tourque = -50f;
    public Transform target;
    [HideInInspector]
    public float steerAngle;
    private float currentAngle = 0;
    private float steeringSpeed = 10.0f;
    private float currentMaxSteerAngle = 20.0f;
    private float maxspeed = 3.0f;
    private Rigidbody rb;
    void Awake()
    {
        steerAngle = 15f;
        Cougar = new WheelCollider[4];
        //set each wheel collider into the cougar array
        Cougar[0] = fl;
        Cougar[1] = fr;
        Cougar[2] = bl;
        Cougar[3] = br;

        rb = GetComponent<Rigidbody>();

    }
    void FixedUpdate()
    {
        for(int i = 0; i < 4; i++)
        {
            //set the tourque for each wheel collider 
            Cougar[i].motorTorque = tourque;
        }
        fl.steerAngle = steerAngle;
        fr.steerAngle = steerAngle;
    }
    void Update()
    {
       
        if(rb.velocity.magnitude >= maxspeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxspeed);// if the cougars speed reaches or exceeds the max speed then set it back to the max speed
        }
        Vector3 moveDirection = (target.position - transform.position).normalized;// set move direction to the targets position - the cougars position
        Debug.DrawRay(transform.position, moveDirection * 150, Color.yellow);
        Vector3 LocalTarget = transform.InverseTransformPoint(target.position); // set local target
        LocalTarget = LocalTarget * -1;
        float targetAngle = Mathf.Atan2(LocalTarget.x, LocalTarget.z) * Mathf.Rad2Deg; // set target angle
        if(currentAngle<targetAngle)
        {
            currentAngle = currentAngle+(Time.deltaTime * steeringSpeed);
            if(currentAngle>targetAngle) // if the current angle exceeds the target angle then set it back to the target
            {
                currentAngle = targetAngle;

            }
        }
        else if(currentAngle>targetAngle)
        {
            currentAngle = currentAngle - (Time.deltaTime * steeringSpeed);
            if(currentAngle<targetAngle)
            {
                currentAngle = targetAngle; // if the current angle is less than the taget angle then set it to the target angle
            }
        }
        steerAngle = Mathf.Clamp(currentAngle, (-1) * currentMaxSteerAngle, currentMaxSteerAngle);
    }
}
