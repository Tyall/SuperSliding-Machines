using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{

    public float TurnSpeed;
    public float Acceleration;

    public float maxSteerAngle = 40f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;

    Quaternion targetRotation;
    public Rigidbody vehicle;

    public Transform path;
    //The path needs to be found by tag or name with each car

    private List<Transform> nodes;
    private int currentNode = 0;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //ApplySteer();
        //Drive();
        CheckWaypointDistance();
        DriveByRigidbody();
    }

    private void DriveByRigidbody()
    {
        
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        vehicle.AddRelativeForce(Vector3.forward * Acceleration * Time.fixedDeltaTime);

        //It's good to this point



        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * TurnSpeed);

        //Vector3 target = new Vector3(joyH * 100f, 0, joyV * 100f); //JAK TO DZIALA XDDD
        Vector3 target = new Vector3((relativeVector.x / relativeVector.magnitude) * maxSteerAngle, 0, (relativeVector.y / relativeVector.magnitude) * maxSteerAngle); 
        Vector3 direction = target - transform.position;
        float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        targetRotation = Quaternion.Euler(0, rotationAngle, 0);
        //Something is not yes
    }

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        wheelFL.motorTorque = Acceleration;
        wheelFR.motorTorque = Acceleration;
        //Make car lighter i guess but it'll break the mass of the car on collisions
        //Probably gotta rebuild it to the way player car is handled
        //Rigidbody velocity instead of motor torque
    }

    private void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.5f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
            
        }
    }
}
