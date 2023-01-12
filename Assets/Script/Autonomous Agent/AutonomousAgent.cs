using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutonomousAgent : Agent
{
    public Perception flockPerception;

    public float wanderDistance = 1;
    public float wanderRadius = 3;
    public float wanderDisplacement = 5;

    [Space(30)]

    [Range(0,10)] public float separationRadius = 3;

    [Space(30)]

    [Range(0, 3)] public float FleeWeight = 0;
    [Range(0, 3)] public float SeekWeight = 1;

    [Space(30)]

    [Range(0, 3)] public float Flock = 1;
    [Range(0, 3)] public float Separate = 1;
    [Range(0, 3)] public float Align = 1;

    public float wanderAngle { get; set; } = 0;
    void Update()
    {
        var gameObjects = perception.GetGameObjects();

        foreach (var GO in gameObjects)
        {
            Debug.DrawLine(transform.position, GO.transform.position);
        }
        if (gameObjects.Length > 0) 
        {
            movement.ApplyForce(Steering.Seek(this, gameObjects[0]) * SeekWeight);
            movement.ApplyForce(Steering.Flee(this, gameObjects[0]) * FleeWeight);
        }

        gameObjects = flockPerception.GetGameObjects();
        if (gameObjects.Length > 0)
        {
            movement.ApplyForce(Steering.Cohesion(this, gameObjects) * Flock);
            movement.ApplyForce(Steering.Separation(this, gameObjects, separationRadius) * Separate);
            movement.ApplyForce(Steering.Alignment(this, gameObjects) * Align);
            //movement.ApplyForce(Steering.Flee(this, gameObjects[0]));
        }

        if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f)
        {
            movement.ApplyForce(Steering.Wander(this));
        }
        transform.position = Utilities.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));
    }
}
