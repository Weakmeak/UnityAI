using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{ 
    public DeathState(StateAgent owner) : base(owner) { }

    public override void OnEnter()
    {
        owner.movement.Stop();
        owner.animator.SetBool("isDead", true);
    }

    public override void OnExit()
    {
        //Debug.Log("Idle: EXIT");
    }

    public override void OnUpdate()
    {
        if (owner.animationDone)
        {
            GameObject.Destroy(owner.gameObject);
        }
    }
}
