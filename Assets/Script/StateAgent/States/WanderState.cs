using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WanderState : State
{
    public WanderState(StateAgent owner) : base(owner) { }

    private Vector3 target;

    public override void OnEnter()
    {
        //Debug.Log("Patrol: ENTER");
        owner.movement.Resume();
        owner.navigation.targetNode = null;
        target = owner.transform.position + Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * Vector3.forward * 5;
    }

    public override void OnExit()
    {
        //Debug.Log("Patrol: EXIT");
    }

    public override void OnUpdate()
    {
        Debug.DrawLine(owner.transform.position, target);
        owner.movement.MoveTowards(target);
        if (owner.GetComponent<Rigidbody>().velocity.magnitude <= 0.5f || Vector3.Distance(owner.transform.position, target) < 0.75) 
        {
            owner.stateMachine.StartState(nameof(IdleState));
        }
    }
}
