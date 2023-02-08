using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PatrolState : State
{
    public PatrolState(StateAgent owner) : base(owner) { }

    private float timer;

    public override void OnEnter()
    {
        //Debug.Log("Patrol: ENTER");
        owner.movement.Resume();
        owner.navigation.targetNode = owner.navigation.GetNearestNode();
        timer = Random.Range(5.0f, 10.0f);
    }

    public override void OnExit()
    {
        //Debug.Log("Patrol: EXIT");
    }

    public override void OnUpdate()
    {
        if (timer <= 0)
        {
            owner.stateMachine.StartState(nameof(WanderState));
        }
        else
        {
            // move towards the perceived object position 
            owner.movement.MoveTowards(owner.perceived[0].transform.position);

            // create a direction vector toward the perceieved object from the owner
            Vector3 direction = owner.perceived[0].transform.position - owner.transform.position;
             // get the distance to the perceived object 
             float distance = direction.magnitude;
            // get the angle between the owner forward vector and the direction vector 
            float angle = Vector3.Angle(owner.transform.forward, direction);
            // if within range and angle, attack 
            if (distance < 2.5 && angle < 20)
            {
                owner.stateMachine.StartState(nameof(AttackState));
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
