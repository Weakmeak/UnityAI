using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Steering 
{
    public static Vector3 Seek(Agent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, (target.transform.position - agent.transform.position));

        force.y = 0f;
    
        return force;
    }

    public static Vector3 Flee(Agent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, (agent.transform.position - target.transform.position));

        force.y = 0f;

        return force;
    }

    public static Vector3 Wander(AutonomousAgent agent)
    {
        // randomly adjust angle +/- displacement 
        agent.wanderAngle = agent.wanderAngle + Random.Range(-
       agent.wanderDisplacement, agent.wanderDisplacement);
        // create rotation quaternion around y-axis (up) 
        Quaternion rotation = Quaternion.AngleAxis(agent.wanderAngle, Vector3.up);
        // calculate point on circle radius 
        Vector3 point = rotation * (Vector3.forward * agent.wanderRadius);
        // set point in front of agent at distance length 
        Vector3 forward = agent.transform.forward * agent.wanderDistance;

        Vector3 force = CalculateSteering(agent, forward + point);

        Debug.DrawRay(agent.transform.position, forward + point, Color.red);

        force.y = 0f;

        return force;
    }

    public static Vector3 Cohesion(Agent agent, GameObject[] neighbors)
    {
        Vector3 center = Vector3.zero;

        foreach (var neigh in neighbors)
        {
            center += neigh.transform.position;
        }

        center /= neighbors.Length;

        Vector3 direction = center - agent.transform.position;

        Vector3 force = CalculateSteering(agent, direction);

        force.y = 0f;

        return force;
    }

    public static Vector3 Separation(Agent agent, GameObject[] neighbors, float sepaRadius)
    {
        return Vector3.zero;
    }

    public static Vector3 Alignment(Agent agent, GameObject[] neighbors)
    { 
        return Vector3.zero;
    }

    public static Vector3 CalculateSteering(Agent agent, Vector3 direction)
    {
        Vector3 ndirection = direction.normalized;
        Vector3 desired = ndirection * agent.movement.maxSpeed;
        Vector3 steer = desired - agent.movement.velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, agent.movement.maxForce);

        return force;
    }
}
