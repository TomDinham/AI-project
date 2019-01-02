using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Senses : MonoBehaviour {
    public GameObject target;
    private CharacterController characterController;
    public float viewingAngle = 200.0f;
    public float sightRange = 200.0f;

    
	void Start ()
    {
        characterController = GetComponent<CharacterController>();
	}
	
	

    
    public bool CanSeeTarget()
    {
        // Check target is still in scene 
        if (target != null) {
            // set the distance to the target 
            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            
            if (sightRange > distanceToTarget)// is the taget within the sight range
            {
                
                Vector3 targetDirection = target.transform.position - transform.position;// set the director the target is compaired to the object
                
                float angle = Vector3.Angle(targetDirection, transform.forward);// the angle between the target and the object
                
                angle = System.Math.Abs(angle);// convert the angle to a positive value
               
                if (angle < (viewingAngle / 2))//is the target witin the viewing angle of the object 
                {
                    CharacterController targetCharacterController = target.GetComponent<CharacterController>();
                    RaycastHit hitData; // set up a raycast to hold return data
                    
                    LayerMask playerMask = 1 << 8;
                    LayerMask aiMask = 1 << 10;
                    
                    LayerMask coverMask = 1 << 9;
                    
                    LayerMask mask = coverMask | playerMask | aiMask;
                    
                    float targetHeight = targetCharacterController.height;// height of the target
                    
                    float height = characterController.height; // height of the object 
                    
                    Vector3 eyePosition = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);// get objects eye position
                    
                    Vector3 targetPos = new Vector3(target.transform.position.x, target.transform.position.y - (targetHeight / 2.0f), target.transform.position.z);
                   
                    Vector3 direction = (targetPos - transform.position).normalized;//set in the middle of the target
                    
                    bool hit = Physics.Raycast(eyePosition, direction, out hitData, sightRange, mask.value);// cast the ray from the posision 
                    Debug.DrawRay(eyePosition, direction * sightRange, Color.red);
                    
                    if (hit)
                    {
                        // if the raycast hit
                        if (hitData.collider.tag == target.gameObject.tag)//if the returned hit tag is the same as the targets
                        {
                            return true;
                        }
                    }

                }
            }
        }
        return false;
    }

}
