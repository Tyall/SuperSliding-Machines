using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AI_Manager : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 startRotation;

    private Vector3 input;

    private NeuralNetwork network;

    [Range(-1f, 1f)]
    public float acceleration, turnAngle;

    public float fitnessScore;
    public float timeSinceStart = 0f;
    private Vector3 lastPosition;
    private float totalDistance;
    private float averageSpeed;
    private float totalTimeSinceStart = 0f;
    private float totalDistanceWithMultiplier = 0f;
    private float averageSpeedWithMultiplier = 0f;
    private float racingLineScore = 0f;

    public float bestFitnessScore;

    [Header("Fitness calculation parameters")]
    public float distanceMultiplier = 1.4f;
    public float averageSpeedMultiplier = 0.2f;
    public float sensorMultiplier = 0.1f;

    [Header("Vehicle parameters")]
    public float maxVehicleAcceleration = 11.5f;
    public float maxVehicleTurnAngle = 90;
    //public float movementSmoothing = 0.02f;
    public float movementSmoothing;

    [Header("Individuals parameters")]
    public float maxIndividualLifeSpan = 20;
    public float minIndividualFitnessScore = 40;
    public float maxIndividualFitnessScore = 1000;

    [Header("Save System")]
    public string filename;
    public int savedNetworks = 0;

    [Range(2,10)]
    public int networkLayers = 2;
    [Range(4, 20)]
    public int networkNeurons = 10;

    public string difficulty = "EASY";

    private float aSensor;
    private float bSensor;
    private float cSensor;
    private float dSensor;
    private float eSensor;


    public TextMeshProUGUI textBestFitness;
    public TextMeshProUGUI textCurrentFitness;
    public TextMeshProUGUI textTimeTotal;
    public TextMeshProUGUI textTimeIndividual;

    RaycastHit gdetectionHit;
    float gdetectionDistance = 10f;
    Vector3 gdetectionDirection = -Vector3.up;
    Vector3 gdetectionOffset = new Vector3(0, 0.3f, 0f);

    //TESTING
    GA_Manager gm;

    private void Awake()
    {
        networkLayers = networkLayers - 2;
        movementSmoothing = Time.fixedDeltaTime;
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
        network = GetComponent<NeuralNetwork>();
        gm = FindObjectOfType<GA_Manager>();
    }

    private void FixedUpdate()
    {
        GetInputFromSensors();
        UpdateLastPosition();
        GetNetworkOutput();
        MoveVehicle(acceleration, turnAngle);
        UpdateTime();
        CalculateFitnessScore();
        CheckForRoad();
        UpdateStatsGUI();

    }

    public void UpdateStatsGUI()
    {
        textBestFitness.text = bestFitnessScore.ToString(("0.00"));
        textCurrentFitness.text = fitnessScore.ToString(("0.00"));
        textTimeTotal.text = totalTimeSinceStart.ToString(("0.00s"));
        textTimeIndividual.text = timeSinceStart.ToString(("0.00s")); 
    }
    private void GetInputFromSensors()
    {
        Vector3 a = (transform.forward + transform.forward + transform.right);
        Vector3 b = (transform.forward + transform.right + transform.right ); //test
        Vector3 c = (transform.forward);
        Vector3 d = (transform.forward - transform.right - transform.right ); //test;
        Vector3 e = (transform.forward + transform.forward - transform.right);

        //Vector3 offset = new Vector3(0, 0.4f, 0f);
        //float maxDistance = 25;

        //Ray ray = new Ray(transform.position + offset, 0);
        //RaycastHit hit;

        aSensor = DrawSensor(a);
        bSensor = DrawSensor(b);
        cSensor = DrawSensor(c);
        dSensor = DrawSensor(d);
        eSensor = DrawSensor(e);
    }

    private float DrawSensor(Vector3 dir)
    {
        RaycastHit hit;

        float maxDistance = 25;
        Vector3 offset = new Vector3(0, 0.4f, 0f);

        Ray ray = new Ray(transform.position + offset, dir);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.green, 0.0f);
            //Gizmos.DrawRay(ray);
            if (hit.collider.tag == "NotRoad")
            {
                float sensorValue = hit.distance / maxDistance;
                Debug.DrawLine(ray.origin, hit.point, Color.red);
                //Gizmos.DrawLine(ray.origin, hit.point);
                return sensorValue;
            }
            return 1f;
        }
        return 1f; 
    }

   public void SetTimeScale(int scale)
    {
        switch (scale)
        {
            case 1:
                Time.timeScale = 1.0f;
                break;
            case 2:
                Time.timeScale = 7.5f;
                break;
            case 3:
                Time.timeScale = 15f;
                break;

        }
    }

    private void UpdateLastPosition()
    {
        lastPosition = transform.position;
    }

    private void GetNetworkOutput()
    {
        (acceleration, turnAngle) = network.RunNetwork(aSensor, bSensor, cSensor, dSensor, eSensor);
    }

    public void MoveVehicle(float acc, float turn)
    {
        input = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, acc * maxVehicleAcceleration), movementSmoothing);
        input = transform.TransformDirection(input);
        transform.position += input;
        transform.eulerAngles += new Vector3(0, (turn * maxVehicleTurnAngle) * movementSmoothing, 0);
    }

    private void UpdateTime()
    {
        timeSinceStart += Time.deltaTime;
        totalTimeSinceStart += Time.deltaTime;
    }
    
    private void CalculateFitnessScore()
    {
        totalDistance += Vector3.Distance(transform.position, lastPosition);
        averageSpeed = totalDistance / timeSinceStart;

        totalDistanceWithMultiplier = (totalDistance * distanceMultiplier);
        averageSpeedWithMultiplier = (averageSpeed * averageSpeedMultiplier);
        racingLineScore = (((aSensor + bSensor + cSensor + dSensor + eSensor) / 5) * sensorMultiplier);

        fitnessScore = totalDistanceWithMultiplier + averageSpeedWithMultiplier + racingLineScore;

        UpdateHighestFitness(fitnessScore);
        FitnessScoreCheck();
    }

    private void UpdateHighestFitness(float fit)
    {
        if (fit >= bestFitnessScore)
        {
            bestFitnessScore = fit;
        }
    }
    private void FitnessScoreCheck()
    {
        if (timeSinceStart > maxIndividualLifeSpan && fitnessScore < minIndividualFitnessScore)
        {
            Death();
        }
        if (fitnessScore >= maxIndividualFitnessScore)
        {
            //Save network to a JSON file
            Debug.Log("NEURAL NETWORK HAS SUCCESFULLY BEEN SAVED TO " + filename);
            SaveNetwork(); //COMMENTED TO SAVE EVERY FILE
            Death();
        }
    }

    /* private void DebugSaveNetwork()
    {
        FindObjectOfType<NetworkSaveManager>().DebugSave(network, filename, networkLayers, networkNeurons, fitnessScore, gm.currentGeneration, gm.currentIndividual );
    }*/

    public void SaveNetwork()
    {
        if (savedNetworks == 30)
        {
            difficulty = "MEDIUM";
            maxIndividualFitnessScore *= 1.8f;
        } else if (savedNetworks == 60)
        {
            difficulty = "HARD";
            maxIndividualFitnessScore *= 1.8f;
        }
        FindObjectOfType<NetworkSaveManager>().Save(network, filename, networkLayers, networkNeurons, difficulty, fitnessScore, totalTimeSinceStart, timeSinceStart, totalDistance, averageSpeed, totalDistanceWithMultiplier, averageSpeedWithMultiplier, racingLineScore, gm.currentGeneration, gm.currentIndividual);
        savedNetworks++;
        //Try to equip a vehicle on track X in network trained on track Y, and see what happens. 
    }

    private void CheckForRoad()
    {
        if (!IsOnRoad())
        {
            Death();
        }
    }

    private bool IsOnRoad()
    {
        if (Physics.Raycast(transform.position + gdetectionOffset, gdetectionDirection, out gdetectionHit, gdetectionDistance))
        {
            if (gdetectionHit.collider.tag == "Road")
            {
                return true;
            }
            else
                return false;
        }
        return false;
    }

    private void Death()
    {
        //FindObjectOfType<NetworkSaveManager>().Save(network, filename, networkLayers, networkNeurons, "TEST_MODE", fitnessScore, totalTimeSinceStart, timeSinceStart, totalDistance, averageSpeed, totalDistanceWithMultiplier, averageSpeedWithMultiplier, racingLineScore, gm.currentGeneration, gm.currentIndividual);
        GameObject.FindObjectOfType<GA_Manager>().Death(fitnessScore, network);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        Death();
    }

    public void ResetStats()
    {
        fitnessScore = 0f; 
        timeSinceStart = 0f;
        lastPosition = startPosition;
        totalDistance = 0f;
        averageSpeed = 0f;
        totalDistanceWithMultiplier = 0f;
        averageSpeedWithMultiplier = 0f;
        racingLineScore = 0f;

        transform.position = startPosition;
        transform.eulerAngles = startRotation;
    }

    public void ResetStatsAndSetNetwork(NeuralNetwork net)
    {
        network = net;
        ResetStats();
    }
}
