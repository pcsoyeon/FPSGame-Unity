using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCtrl : MonoBehaviour
{
    public float rotSpeed = 100;

    Vector3 AXIS_Y = new Vector3(0, 1, 0);

    Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    
    void Update()
    {
        Vector3 rotY = AXIS_Y * rotSpeed * Time.deltaTime;
        tr.Rotate(rotY, Space.World);
    }
}
