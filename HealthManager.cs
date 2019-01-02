using UnityEngine;
using System.Collections;
using System;

public class HealthManager : MonoBehaviour {
    public enum BodyParts { Legs = 20, Waiste = 45, Torso = 65, Head = 90 }
    public enum ModelBodyParts { Invalid = 0, Head = 101, Spine = 40, LeftUpperLeg = 15, LeftLowerLeg = 15, RightUpperLeg = 15, RightLowerLeg = 15, LeftUpperArm = 20, LeftLowerArm = 20, RightUpperArm = 20, RightLowerArm = 20 }

    public enum Probability { Poor = 1, Fair = 2, Good = 3, Excellent = 4 }
    private const int MAX_HEALTH = 100;
    private const int HEALTH_INCREMENT = 1;
    private const float HEALTH_UPDATE_TIME_STEP = 2f;
    public int health;
    public int Health { get { return health; } }
    public int MaxHealth { get { return MAX_HEALTH; } }
  
    public AudioClip[] hitSounds;
    private CharacterController characterController;
    private CapsuleCollider collider;
    private Animator animator;
    private AudioSource audio;
    public int armour = 0;
    public GameObject impactBlood;


    void Awake()
    {
        // give reference to the objects components
        characterController = GetComponent<CharacterController>();
        collider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    public void Start() {
        StartCoroutine(Repair());
    }

    public bool IsDead()
    {
        return (health == 0);
    }

    public void ApplyHealth(int health)
    {
        if ((this.health + health) > MAX_HEALTH)
        {
            //set health to max health if health is more than max health 
            this.health = MAX_HEALTH;
        }
        else
        {
            //incriment health by 0
            this.health = this.health + 0;
        }

    }

    public void ApplyDamage(int damage)
    {
        int potentialDamage = Mathf.RoundToInt(damage * (1.0f - (armour / 100.0f)));
        if (potentialDamage >= health) health = 0; // apply damage of the data recived to the object attached 
        else health = health - potentialDamage;
    }

    IEnumerator Repair()
    {
        ApplyHealth(HEALTH_INCREMENT);
        yield return new WaitForSeconds(HEALTH_UPDATE_TIME_STEP);
        if (!IsDead()) {
            StartCoroutine(Repair());
        }
    }

    public void OnRaycastHit(RaycastHit hitData)
    {
        Debug.Log("Ray Cast Hit");
        // get the names of the bodey parts hit from the raycast data sent in
        foreach (string bp in Enum.GetNames(typeof(ModelBodyParts))) {
            if (bp.Equals(hitData.collider.tag)) {
                InflictDamage((ModelBodyParts)Enum.Parse(typeof(ModelBodyParts), bp));
                GameObject blood = Instantiate(impactBlood, hitData.transform.position, hitData.transform.rotation) as GameObject;
                return;
            }
        }
    }



    private void InflictDamage(ModelBodyParts bp)
    {
        Debug.Log("Damage To " + bp.ToString() + " Apply Damage : " + (int)bp);
        ApplyDamage((int)bp);
    }


    private BodyParts GetBodyPartHit(Vector3 impactPoint) {
        
        Vector3 hitLocation = gameObject.transform.InverseTransformPoint(impactPoint);
        float height = 0f;
        //get the height of the collider or character controller on the object
        if (collider)
        {
            height = collider.height;
        }
        else if (characterController)
        {
            height = characterController.height;
        }
        else Debug.LogError("AI and players should either have a CharacterController or CapsuleCollider component");
        // Preturn point of impact
        float locationY = ((hitLocation.y / height) * hitLocation.y) * 100.0f;
        if (locationY > (int)BodyParts.Head)
            return BodyParts.Head;
        else if (locationY > (int)BodyParts.Torso)
            return BodyParts.Torso;
        if (locationY > (int)BodyParts.Waiste)
            return BodyParts.Waiste;
        else
            return BodyParts.Legs;
    }

    private void InflictDamage(BodyParts bp, DamageData data) {
        // Apply damage based on which body part was hit
        switch (bp) {
            case BodyParts.Head: ApplyDamage(100);
                break;
            case BodyParts.Torso: ApplyDamage(50);
                break;
            case BodyParts.Waiste: ApplyDamage(30);
                break;
            case BodyParts.Legs: ApplyDamage(20);
                break;
        }
        if ((hitSounds.Length > 0) && (!audio.isPlaying)) {
            int soundIndex = UnityEngine.Random.Range(0, hitSounds.Length);
            audio.PlayOneShot(hitSounds[soundIndex], 0.6f);
        }
    }

    public void OnRaycastHit(DamageData data)
    {
        InflictDamage(GetBodyPartHit(data.impactPoint), data);
    }


    public void Die()
    {
        // if the object has lost all its heath then disable animator and character collider
        characterController.enabled = false;
        animator.enabled = false;
        Rigidbody[] rbs = transform.root.gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            //for each ridged body, set gravity to true and is kinematic to dalse
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        Collider[] cols = transform.root.gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            col.isTrigger = false;
            //set the collider to false
        }
    }


}
