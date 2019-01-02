using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAIController : MonoBehaviour
{
   [ HideInInspector]
    public Animator animator;
    [HideInInspector]
    public  CharacterController characterController;
    private Vector3 moveDirection;
    [HideInInspector]
    public Senses senses;
    public float turnSpeed = 2.0f;
    public float forwardSpeed = 2f;
    protected HealthManager healthManager;

    // Use this for initialization
    public void Start ()
    {
       
        healthManager = GetComponent<HealthManager>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        senses = GetComponent<Senses>();
        Rigidbody[] rbs = transform.root.gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            // sets the ridged bodys of the object, gravity to false and is kinematic to false
            rb.isKinematic = true;
            rb.useGravity = false;
        }

	}
	
	
	void Update ()
    {
        if (senses.CanSeeTarget())
        { //if the senses can see the target
            Debug.Log("Can See Player");
            animator.SetTrigger("Surrender");
        }
        
	}
}
