using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PatrolState : State
{
    public PatrolState(StateAgent owner) : base(owner) { }

    public override void OnEnter()
    {
        //Debug.Log("Patrol: ENTER");
        owner.movement.Resume();
        owner.navigation.targetNode = owner.navigation.GetNearestNode();
        owner.timer.value = Random.Range(5.0f, 10.0f);
    }

    public override void OnExit()
    {
        //Debug.Log("Patrol: EXIT");
    }

    public override void OnUpdate()
    {
        //Nothin
    }
}
