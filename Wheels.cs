using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels : MonoBehaviour
{
    public Transform wheelModel;
    private WheelCollider wheelCollider;
    private Vector3 position;
    private Quaternion rotation;
    void Awake()
    {
        wheelCollider = GetComponent<WheelCollider>();
        
    }
    void FixedUpdate()
    {
        wheelCollider.GetWorldPose(out position, out rotation);//get the wheel colliders position and rotation
        wheelModel.position = new Vector3(position.x, position.y, position.z);//get the wheelmodels position and set into teh position vector 3
        wheelModel.transform.rotation = rotation;// set the wheels rotation to the quaternion rotation
        
    }

}
