using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{

    public float TurnSpeed;
    public float Acceleration;

    public float maxSteerAngle = 40f;

    Quaternion targetRotation;
    public Rigidbody vehicle;

    public Transform path;
    //The path needs to be found by tag or name with each car

    private List<Transform> nodes;
    private int currentNode = 0;

    public int AIcheckpointsPassed;
    public bool isInFront;
    public static bool isFront; // This can't be static. Gotta solve it differently

    // Start is called before the first frame update
    void Start()
    {
        path = GameObject.Find("AI_Path").transform;
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
        CheckWaypointDistance();
        DriveByRigidbody();
    }

    private void DriveByRigidbody()
    {
        
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        //print(relativeVector);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;

        vehicle.AddRelativeForce(Vector3.forward * Acceleration * Time.deltaTime);

        Vector3 target = nodes[currentNode].position;
        Vector3 direction = target - transform.position;
        float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        targetRotation = Quaternion.Euler(0, rotationAngle, 0);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * TurnSpeed);


    }



    private void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 2f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
            //Debug.Log("Switching node to " + currentNode);
        }
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Checkpoint")
        {
            
           CheckpointHandler(hit.name);
            
        }

    }

    public void CheckpointHandler(string cpName)
    {
       // Debug.Log("AI Entered " + cpName);
        AIcheckpointsPassed++;
        //MeasurePosition();
    }

    /*public static void MeasurePosition()
    {
        if (isFront == false)
        {
            if (AIcheckpointsPassed > RaceLevelLogic.totalCheckpointsPassed)
            {
                if (RaceLevelLogic.currentPosition <= RaceLevelLogic.numberOfEnemies)
                {
                    RaceLevelLogic.currentPosition++;
                    isFront = true;
                    print("rll pos: " + RaceLevelLogic.currentPosition);
                }
            }
        }
        
    }*/

}
