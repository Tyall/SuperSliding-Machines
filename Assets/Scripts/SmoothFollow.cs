using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    Vector3 offset;

    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("PlayerVehicle").GetComponent<Transform>();
        offset = transform.position - target.position;
    }
   
        
    // Update is called once per frame
    void LateUpdate()
    {
        //target = GameObject.Find("PlayerVehicle").GetComponent<Transform>();
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

}

