using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour
{
    [Range(1, 40)] public float distance = 1.5f;
    [Range(0 , 180)] public float FOV = 45;

    public string TagName = "";

    public GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, distance);

        foreach (var col in colliders)
        {

            if (col.gameObject == gameObject) continue;

            if(TagName == "" || col.CompareTag(TagName))
            {
                Vector3 direction = (col.transform.position - transform.position).normalized;
                float cos = Vector3.Dot(transform.forward, direction);
                float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;
                if(angle <= FOV) result.Add(col.gameObject);
            }
        }

        result.Sort(CompareDistance);

        return result.ToArray();
    }

    public int CompareDistance(GameObject a, GameObject b)
    {
        float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredRangeA.CompareTo(squaredRangeB);
    }
}
