using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EvadeState : State
{
    public EvadeState(StateAgent owner) : base(owner) { }


    public override void OnEnter()
    {
        owner.navigation.targetNode = null;
        owner.movement.Resume();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        if (owner.enemySeen)
        {
            Vector3 direction = owner.transform.position - owner.perceived[0].transform.position;
            owner.movement.MoveTowards(Vector3.Normalize(owner.transform.position + (direction * 5)));
        }
    }
}
