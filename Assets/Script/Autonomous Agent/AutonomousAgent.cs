using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutonomousAgent : Agent
{
    public Perception flockPerception;
    public ObstacleAvoidance obstacleAvoidance;
    public AutonomousAgentData data;

    public float wanderAngle { get; set; } = 0;
    void Update()
    {
        var gameObjects = perception.GetGameObjects();

        /*foreach (var GO in gameObjects)
        {
            Debug.DrawLine(transform.position, GO.transform.position);
        }*/
        if (gameObjects.Length > 0) 
        {
            //Debug.DrawLine(transform.position, gameObjects[0].transform.position);
            movement.ApplyForce(Steering.Seek(this, gameObjects[0]) * data.seekWeight);
            movement.ApplyForce(Steering.Flee(this, gameObjects[0]) * data.fleeWeight);
        }

        gameObjects = flockPerception.GetGameObjects();
        if (gameObjects.Length > 0)
        {
            movement.ApplyForce(Steering.Cohesion(this, gameObjects) * data.cohesionWeight);
            movement.ApplyForce(Steering.Separation(this, gameObjects, data.separationRadius) * data.separationWeight);
            movement.ApplyForce(Steering.Alignment(this, gameObjects) * data.alignmentWeight);
            //movement.ApplyForce(Steering.Flee(this, gameObjects[0]));
        }

        if (obstacleAvoidance.IsObstacleInFront())
        {
            Vector3 direction = obstacleAvoidance.GetOpenDirection();
            movement.ApplyForce(Steering.CalculateSteering(this, direction) * data.obstacleWeight);
        }

        if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f)
        {
            //movement.ApplyForce(Steering.Wander(this));
        }
        transform.position = Utilities.Wrap(transform.position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));
    }
}
