using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;
using UnityEngine.TestTools;

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
        agent.wanderAngle = agent.wanderAngle + Random.Range(-agent.data.wanderDisplacement, agent.data.wanderDisplacement);
        // create rotation quaternion around y-axis (up) 
        Quaternion rotation = Quaternion.AngleAxis(agent.wanderAngle, Vector3.up);
        // calculate point on circle radius 
        Vector3 point = rotation * (Vector3.forward * agent.data.wanderRadius);
        // set point in front of agent at distance length 
        Vector3 forward = agent.transform.forward * agent.data.wanderDistance;

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

    public static Vector3 Separation(Agent agent, GameObject[] neighbors, float radius)
    {
        Vector3 separation = Vector3.zero;
        // accumulate separation vector of neighbors 
        foreach (GameObject neighbor in neighbors)
        {
            // create separation direction (neighbor position <- agent position) 
            Vector3 direction = agent.transform.position-neighbor.transform.position;
            if (direction.magnitude < agent.GetComponent<AutonomousAgent>().data.separationRadius) 
            {
                // scale direction by distance (closer = stronger) 
                separation += direction / direction.sqrMagnitude;
            }
        }

        // steer toward separation 
        Vector3 force = CalculateSteering(agent, separation);

        force.y = 0f;

        return force;
    }

    public static Vector3 Alignment(Agent agent, GameObject[] neighbors)
    {
        Vector3 averageVelocity = Vector3.zero;
        // accumulate velocity of neighbors (velocity = forward direction movement) 
        foreach (GameObject neighbor in neighbors)
        {
            // need to get the Agent component of the game object and then movement velocity 
            if (neighbor.GetComponent<Agent>())
                averageVelocity += neighbor.GetComponent<Agent>().movement.velocity;
        }
        // calculate the average by dividing the average velocity by the number of neighbors
        //< divide average velocity by number of neighbors>

        averageVelocity /= neighbors.Length;

        // steer towards the average velocity of the neighbors 
         Vector3 force = CalculateSteering(agent, averageVelocity);

        return force;
    }

    public static Vector3 CalculateSteering(Agent agent, Vector3 direction)
    {
        Vector3 ndirection = direction.normalized;
        Vector3 desired = ndirection * agent.movement.maxSpeed;
        Vector3 steer = desired - agent.movement.velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, agent.movement.maxForce);

        force.y = 0f;

        return force;
    }
}
