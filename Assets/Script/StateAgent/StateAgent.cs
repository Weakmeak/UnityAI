using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAgent : Agent
{
    public StateMachine stateMachine = new StateMachine();
    public GameObject[] perceived; 
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine.AddState(new IdleState(this));
        stateMachine.AddState(new PatrolState(this));
        stateMachine.AddState(new ChaseState(this));
        stateMachine.AddState(new WanderState(this));
        stateMachine.AddState(new AttackState(this));
        stateMachine.StartState(nameof(IdleState));
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        perceived = perception.GetGameObjects();
        stateMachine.Update();

        if (navigation.targetNode != null)
        {
            movement.MoveTowards(navigation.targetNode.transform.position);
        }

        animator.SetFloat("Speed", movement.velocity.magnitude);

        Vector3 lookDir = movement.velocity;
        lookDir.Normalize();
        lookDir.y = 0;
        gameObject.transform.rotation = Quaternion.LookRotation(lookDir);
    }

    private void OnGUI()
    {
        Vector3 point = cam.WorldToScreenPoint(transform.position);
        GUI.backgroundColor = Color.black;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        Rect rect = new Rect(0, 0, 100, 20);
        rect.x = point.x - (rect.width / 2);
        rect.y = Screen.height - point.y - rect.height - 20;
        GUI.Label(rect, stateMachine.currentState.name);
    }
}
