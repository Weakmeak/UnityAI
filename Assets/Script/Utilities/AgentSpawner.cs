using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    public Agent[] agents;
    public LayerMask layerMask;

    int index = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftBracket)) index = --index % agents.Length;
        if(Input.GetKeyDown(KeyCode.RightBracket)) index = ++index % agents.Length;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
            {
                if (index < agents.Length) Instantiate(agents[index], hitInfo.point, Quaternion.AngleAxis(Random.Range(0,360), Vector3.up));
                else Instantiate(agents[0], hitInfo.point, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up));

            }
        }
    }
}
