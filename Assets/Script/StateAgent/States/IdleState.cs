using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{ 
    public IdleState(StateAgent owner) : base(owner) { }

    public override void OnEnter()
    {
        //Debug.Log("Idle: ENTER");
        owner.timer.value = Random.Range(1f, 3f);
        owner.movement.Stop();
    }

    public override void OnExit()
    {
        //Debug.Log("Idle: EXIT");
    }

    public override void OnUpdate()
    {
    }
}
