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
        if(Input.GetKeyDown(KeyCode.Alpha1)) index = 0;
        if(Input.GetKeyDown(KeyCode.Alpha2)) index = 1;
        if(Input.GetKeyDown(KeyCode.Alpha3)) index = 2;
        if(Input.GetKeyDown(KeyCode.Alpha4)) index = 3;
        if(Input.GetKeyDown(KeyCode.Alpha5)) index = 4;
        if(Input.GetKeyDown(KeyCode.Alpha6)) index = 5;
        if(Input.GetKeyDown(KeyCode.Alpha7)) index = 6;
        if(Input.GetKeyDown(KeyCode.Alpha8)) index = 7;
        if(Input.GetKeyDown(KeyCode.Alpha9)) index = 8;
        if(Input.GetKeyDown(KeyCode.Alpha0)) index = 9;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
            {
                if (index < agents.Length) Instantiate(agents[index], hitInfo.point, Quaternion.identity);
                else Instantiate(agents[0], hitInfo.point, Quaternion.identity);

            }
        }
    }
}
