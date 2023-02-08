using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private float actionTimer = 0f;
    public IdleState(StateAgent owner) : base(owner) { }

    public override void OnEnter()
    {
        //Debug.Log("Idle: ENTER");
        actionTimer = Random.Range(1f, 3f);
        owner.movement.Stop();
    }

    public override void OnExit()
    {
        //Debug.Log("Idle: EXIT");
    }

    public override void OnUpdate()
    {
        //Debug.Log("Idle: UPDATE");
        actionTimer -= Time.deltaTime;
        if (actionTimer < 0f)
        {
            owner.stateMachine.StartState(nameof(PatrolState));
        }
    }
}
