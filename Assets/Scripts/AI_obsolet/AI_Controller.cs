using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NeuralNet))]
public class AI_Controller : MonoBehaviour
{
    //public float TurnSpeed;
    //public float Acceleration;
    //public float maxSteerAngle = 40f;

    //public Rigidbody vehicle;

    private Vector3 startPosition, startRotation;
    private NeuralNet network;

    [Range(-1f, 1f)]
    public float Acceleration, TurnAngle; //Acceleration and turning values

    public float timeSinceStart = 0f;

    [Header("Fitness")] //Indicates how well the car performs
    public float overallFitness;
    public float distanceMultiplier = 1.4f; //How important the distance is to the overall fitness score | To be tweaked
    public float avgSpeedMultiplier = 0.2f; //How important the avg speed is to the overall fitness score | To be tweaked
    public float sensorMultiplier = 0.1f; //How important it is to stay in the middle of the car | To be tweaked

    [Header("Network Options")] // To be tweaked
    public int LAYERS = 1;
    public int NEURONS = 10;

    private Vector3 lastPosition;
    private float totalDistanceTravelled;
    private float avgSpeed;

    private float aSensor, bSensor, cSensor; //Neural Network inputs | To be tweaked

    //Ground detection
    RaycastHit gdHit;
    float gdDistance = 10f;
    Vector3 gdDir = -Vector3.up;
    Vector3 gdOffset = new Vector3(0, 0.3f, 0f);

    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
        network = GetComponent<NeuralNet>();

    }

    public void ResetWithNetwork(NeuralNet net)
    {
        network = net;
        Reset();
    }

    public void Reset()
    {
        timeSinceStart = 0f;
        totalDistanceTravelled = 0f;
        avgSpeed = 0f;
        lastPosition = startPosition;
        overallFitness = 0f;
        transform.position = startPosition;
        transform.eulerAngles = startRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Death();
    }

    private void FixedUpdate()
    {
        InputSensors();
        lastPosition = transform.position;

        (Acceleration, TurnAngle) = network.RunNetwork(aSensor, bSensor, cSensor);

        MoveCar(Acceleration, TurnAngle);

        timeSinceStart += Time.deltaTime;

        CalculateFitness();

        if (!CheckForRoad()){
            Death();
        }

        //a = 0;
        //t = 0;

    }

    private void Death()
    {
        GameObject.FindObjectOfType<GeneticManager>().Death(overallFitness, network);
    }
    private void CalculateFitness()
    {
        totalDistanceTravelled += Vector3.Distance(transform.position, lastPosition);
        avgSpeed = totalDistanceTravelled / timeSinceStart;

        overallFitness = (totalDistanceTravelled * distanceMultiplier) + (avgSpeed * avgSpeedMultiplier) + (((aSensor + bSensor + cSensor) / 3) * sensorMultiplier);
        


        if (timeSinceStart > 20 && overallFitness < 40) //values to be tweaked
        {
            Death();
        }
        //
        if (overallFitness >= 1000) //values to be tweaked
        {
            //Save network to a JSON file
            Death();
        }

    }
    /*private void InputSensors_original()
    {
        Vector3 a = (transform.forward + transform.right);
        Vector3 b = (transform.forward);
        Vector3 c = (transform.forward - transform.right);

        Vector3 offset = new Vector3(0, 0.4f, 0f);

        Ray r = new Ray(transform.position + offset, a);
        RaycastHit hit;

        //Add a tag to check if it hit wall/ground instead of random objects like checkpoints

        if(Physics.Raycast(r, out hit))
        {
            aSensor = hit.distance / 20; //We have to normalize the value before we pass it to the neural network <they have to be a range between 0 and ~1f>
            print("A : " + aSensor);
            Debug.DrawLine(r.origin, hit.point, Color.red);

        }

        r.direction = b;

        if (Physics.Raycast(r, out hit))
        {
            bSensor = hit.distance / 20; 
            print("B : " + aSensor);
            Debug.DrawLine(r.origin, hit.point, Color.red);

        }

        r.direction = c;

        if (Physics.Raycast(r, out hit))
        {
            cSensor = hit.distance / 20;
            print("C : " + aSensor);
            Debug.DrawLine(r.origin, hit.point, Color.red);

        }

    }*/

    private void InputSensors()
    {
        Vector3 a = (transform.forward + transform.right);
        Vector3 b = (transform.forward);
        Vector3 c = (transform.forward - transform.right);

        Vector3 offset = new Vector3(0, 0.4f, 0f);

        Ray r = new Ray(transform.position + offset, a);
        RaycastHit hit;

        float maxDistance = 25;

        //Add a tag to check if it hit wall/ground instead of random objects like checkpoints

        if (Physics.Raycast(r, out hit, maxDistance))
        {
            Debug.DrawRay(r.origin, r.direction * 10, Color.green, 0.0f);
            if (hit.collider.tag == "NotRoad")
            { //DEBUG: any hit
                aSensor = hit.distance / 20; //We have to normalize the value before we pass it to the neural network <they have to be a range between 0 and ~1f>
                print("A : " + aSensor);
                Debug.DrawLine(r.origin, hit.point, Color.red);
            }
        }

        r.direction = b;

        
        if (Physics.Raycast(r, out hit, maxDistance))
        {
            Debug.DrawRay(r.origin, r.direction * 10, Color.green, 0.0f);
            if (hit.collider.tag == "NotRoad")
            {
                bSensor = hit.distance / 20;
                print("B : " + aSensor);
                Debug.DrawLine(r.origin, hit.point, Color.red);
            }
        }

        r.direction = c;

        
        if (Physics.Raycast(r, out hit, maxDistance))
        {
            Debug.DrawRay(r.origin, r.direction * 10, Color.green, 0.0f);
            if (hit.collider.tag == "NotRoad")
            {
                cSensor = hit.distance / 20;
                print("C : " + aSensor);
                Debug.DrawLine(r.origin, hit.point, Color.red);
            }
        }   
    }

    private bool CheckForRoad()
    {
        if (Physics.Raycast(transform.position + gdOffset, gdDir, out gdHit, gdDistance))
        {
            if (gdHit.collider.tag == "Road")
            {
                return true;
            }
            else
                return false;
        }
        return false;
    }

    private Vector3 input;
    public void MoveCar(float v, float h) //Vertical and Horizontal | To be tweaked
    {
        input = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, v * 11.4f), 0.02f); //TODO: make each car have different speed values etc
        input = transform.TransformDirection(input); //Convert input to position relative to the car
        transform.position += input;

        transform.eulerAngles += new Vector3(0, (h * 90) * 0.02f, 0); //TODO: tweak the value of steering smoothing <0.02f for now>


    }

}
