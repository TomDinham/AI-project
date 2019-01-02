using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateDrivenBrain : BasicAIController
{
    public enum Zstates {Idle, FindPlayer, Attack};
    public FSM<Zstates> ZStateMachine;
    public float thinkInterval = 0.5f;
    [HideInInspector]
    public NavMeshAgent nav;
    public float speed = 0.0f;
    public float walkSpeed = 0.05f;


    void Awake ()
    {
        // set up the state machine with its states and transistions
        nav = GetComponent<NavMeshAgent>();

        ZStateMachine = new FSM<Zstates>();
        ZStateMachine.AddState(new Idle<Zstates>(Zstates.Idle, this, 2f));
        ZStateMachine.AddState(new FindPlayer<Zstates>(Zstates.FindPlayer, this, 0f));
        ZStateMachine.AddState(new Attack<Zstates>(Zstates.Attack, this, 1f));

        ZStateMachine.SetInitialState(Zstates.Idle);

        ZStateMachine.AddTransition(Zstates.Idle, Zstates.FindPlayer);
        ZStateMachine.AddTransition(Zstates.FindPlayer, Zstates.Attack);
        ZStateMachine.AddTransition(Zstates.Attack, Zstates.Idle);
	}
    public bool GuardIdleToFindPlayer(State<Zstates> currentState)
    {
        return (true);

    }
    public bool GuardFindPlayerToAttack(State<Zstates> currentState)
    {
        return (this.transform.position == senses.target.transform.position);
    }
    public bool GuardAttackToIdle(State<Zstates> currentState)
    {
        return (true);
    }
    protected void Start()
    {
        base.Start();
        StartCoroutine(Think());
    }

    void Update ()
    {
        if (ZStateMachine != null)
        {
            ZStateMachine.CurrentState.Act(); //  cheack for state machine and send to the currents state act function
        }
        if (healthManager.IsDead())
        {
            healthManager.Die(); // check if the object attacked has got health or is dead
            return;
        }

    }
    protected virtual IEnumerator Think()
    {
        yield return new WaitForSeconds(thinkInterval);
        if (ZStateMachine != null)
        {
            ZStateMachine.Check(); // check for state machine 
        }
        StartCoroutine(Think());
    }

}
