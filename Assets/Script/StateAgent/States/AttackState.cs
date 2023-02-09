using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class AttackState : State
{
    private float timer;

    public AttackState(StateAgent owner) : base(owner)
    {
    }

    public override void OnEnter()
    {
        owner.navigation.targetNode = null;
        owner.movement.Stop();
        owner.animator.SetTrigger("Attack");

        AnimationClip[] clips = owner.animator.runtimeAnimatorController.animationClips;

        AnimationClip clip = clips.FirstOrDefault<AnimationClip>(clip => clip.name == "Punch");
        timer = (clip != null) ? clip.length : 1;

        var collisions = Physics.OverlapSphere(owner.transform.position, 1.5f);

        foreach (var collision in collisions)
        {
            if (collision.gameObject == collision.gameObject || collision.gameObject.CompareTag(owner.gameObject.tag)) continue;

            collision.gameObject.TryGetComponent<StateAgent>(out var saComponent);

            saComponent.health.value -= Random.RandomRange(25, 63);
        }
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) 
        {
            owner.stateMachine.StartState(nameof(ChaseState));
        }
    }
}
