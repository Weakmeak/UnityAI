using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent
{
    void Update()
    {
        var gameObjects = perception.GetGameObjects();

        foreach (var GO in gameObjects)
        {
            Debug.DrawLine(transform.position, GO.transform.position);
        }
    }
}
