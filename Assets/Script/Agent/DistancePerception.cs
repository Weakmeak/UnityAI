using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistancePerception : Perception
{
    public override GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, distance);

        foreach (var col in colliders)
        {

            if (col.gameObject == gameObject) continue;

            if (tagName == "" || col.CompareTag(tagName))
            {
                Vector3 direction = (col.transform.position - transform.position).normalized;
                float cos = Vector3.Dot(transform.forward, direction);
                float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;
                if (angle <= maxAngle) result.Add(col.gameObject);
            }
        }

        result.Sort(CompareDistance);

        return result.ToArray();
    }

}
