using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuzzy : BasicAIController
{
    public enum fuzzy { flee, still };
    public FSM<fuzzy> fuzStateMachine;
    public float thinkInterval = 0.5f;
    public Transform fleePoint;

    public int healthLevel;
    public float range;

    private int fuzzyHealth;
    private int fuzzyRange;

    public int degreeFlee;
    public int degreestill;
    public int fuzzyActionValue;

    void Awake()
    {
       // set up the state machine with the transitions
        fuzStateMachine = new FSM<fuzzy>();
        fuzStateMachine.AddState(new still<fuzzy>(fuzzy.still, this, 0f));
        fuzStateMachine.AddState(new flee<fuzzy>(fuzzy.flee, this, 5f));
        

        fuzStateMachine.SetInitialState(fuzzy.still);

        fuzStateMachine.AddTransition(fuzzy.still, fuzzy.flee);
        fuzStateMachine.AddTransition(fuzzy.flee, fuzzy.still);
       
    }
    public bool GuardstillToflee(State<fuzzy> currentState)
    {
        return (fuzzyActionValue <= degreeFlee); // if the fuzzy actions value is the same or less than the degree flee

    }
    public bool GuardfleeTostill(State<fuzzy> currentState)
    {
        return (fuzzyActionValue >= degreestill);//if the fuzzy action value is more than or the same as the degree still
    }
   
    protected void Start()
    {
        base.Start();
        StartCoroutine(Think());
    }

    void Update()
    {
        if (fuzStateMachine != null)
        {
            fuzStateMachine.CurrentState.Act(); // check for state machine and send to its act function
        }
    }
    protected virtual IEnumerator Think()
    {
        healthLevel = healthManager.Health;
        range = (transform.position - senses.target.transform.position).magnitude; // set the range as the objects transform - the targets transform


        fuzzyRange = fuzzyLogic.Grade(range, 1,15); // if the target gets within 1 to 15 units of the object increas the fuzzy range value
        fuzzyHealth = fuzzyLogic.Grade(healthLevel, 25, 100); //if the health of the object is between 25 or 100

        degreeFlee = fuzzyLogic.AND(fuzzyLogic.NOT(fuzzyRange), fuzzyLogic.NOT(fuzzyHealth)); // set the value of degree flee to the higher of the two values of fuzzy range and health
        degreestill = fuzzyLogic.OR(fuzzyRange, fuzzyLogic.NOT(fuzzyHealth)); // returns the inverse of the two valuse 
        fuzzyActionValue = fuzzyLogic.OR(degreeFlee, degreestill); // sets the action value of the lower value of the two 

        yield return new WaitForSeconds(thinkInterval);
        if (fuzStateMachine != null)
        {
           fuzStateMachine.Check();  // checks state machine 
        }
        StartCoroutine(Think());
    }
}
