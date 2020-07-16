using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SteeringScript : MonoBehaviour
{
    [SerializeField] float turnSpeed = 5;
    [SerializeField] float acceleration = 8;

    Quaternion targetRotation;
    Rigidbody vehicle;

    Vector3 lastPosition;
  


    protected Joystick joystick;
    float joyH;
    float joyV;


    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        vehicle = GetComponent<Rigidbody>();
    }

    

    void GetJoystickPosition()
    {
        joyH = joystick.Horizontal;
        joyV = joystick.Vertical;
    }

    void Update()
    {
      
        SetRotationPoint();
       
        
    }

  



    void FixedUpdate()
    {
        GetJoystickPosition();
                     
        // float accelerationInput = acceleration * (Input.GetMouseButton(0) ? 1 : Input.GetMouseButton(1) ? -1 : 0) * Time.fixedDeltaTime;
        float accelerationInput = acceleration * ((joyH != 0 || joyV != 0) ? 1 : (joyH == 0 || joyV == 0) ? 0 : 0) * Time.fixedDeltaTime;

        vehicle.AddRelativeForce(Vector3.forward * accelerationInput );

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * turnSpeed );

    }

    private void SetRotationPoint()
    {
        
        if (joyH!=0 || joyV!=0)
        {
            Vector3 target = new Vector3(joyH*100f, 0, joyV*100f); //JAK TO DZIALA XDDD
            Vector3 direction = target - transform.position;
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0, rotationAngle, 0);
        }

    }


}