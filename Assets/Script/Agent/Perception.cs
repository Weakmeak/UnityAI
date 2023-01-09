using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour
{
    [Range(1, 40)] public float distance = 1.5f;
    [Range(0 , 180)] public float FOV = 45;

    public GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, distance);

        foreach (var col in colliders)
        {
            if (col.gameObject == gameObject) continue;

            result.Add(col.gameObject);
        }

        

        return result.ToArray();
    }
}
