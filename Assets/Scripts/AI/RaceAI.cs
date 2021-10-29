using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using System;
using Random = UnityEngine.Random;
public class RaceAI : MonoBehaviour
{
    //Start - Load from file
    //Start - ReCreate network
    //FixedUpdate - Cast raycast
    //FixedUpdate - Run Network
    //FixedUpdate - MoveVehcle
    [HideInInspector]
    public Matrix<float> inputLayer = Matrix<float>.Build.Dense(1, 5); 
    public Matrix<float> outputLayer = Matrix<float>.Build.Dense(1, 2);
    [HideInInspector]
    public List<Matrix<float>> hiddenLayers = new List<Matrix<float>>(); //TO BE LOADED
    public List<Matrix<float>> weights = new List<Matrix<float>>(); //TO BE LOADED
    [HideInInspector]
    public List<float> biases = new List<float>(); //TO BE LOADED
    [HideInInspector]
    public int numberOfInputs = 5;
    [HideInInspector]
    public int numberOfOutputs = 2;
    [Range(-1f, 1f)]
    public float acceleration, turnAngle;
    [Header("Vehicle parameters")]
    public float maxVehicleAcceleration = 11.5f; //Modified by each car values
    public float maxVehicleTurnAngle = 90; //Modified by each car values
    //public float movementSmoothing = 0.02f; 
    public float movementSmoothing;

    [Header("Save System")]
    public string filename;

    [Range(2, 10)]
    public int networkLayers = 2;
    [Range(4, 20)]
    public int networkNeurons = 10;

    private Vector3 input;

    private float aSensor;
    private float bSensor;
    private float cSensor;
    private float dSensor;
    private float eSensor;

    RaycastHit gdetectionHit;
    float gdetectionDistance = 10f;
    Vector3 gdetectionDirection = -Vector3.up;
    Vector3 gdetectionOffset = new Vector3(0, 0.3f, 0f);

    private void Start()
    {
        movementSmoothing = Time.fixedDeltaTime;
        LoadNetwork();
       // RecreateNetwork();
    }

    private void FixedUpdate()
    {
        //If it won't work - add condition to check if bool netReacreated = 1 then run loop

        GetInputFromSensors();
        GetNetworkOutput();
        MoveVehicle(acceleration, turnAngle);
        //CheckForRoad();
    }

    public void LoadNetwork()
    {
        //Load button triggers that
        NetworkSaveManager nsm = FindObjectOfType<NetworkSaveManager>();

        nsm.Load(filename); //filename eg Track1_AI_Easy
        networkLayers = nsm.networkLayers - 2;
        networkNeurons = nsm.networkNeurons;
        //Debug.Log("layers " + networkLayers);
        //Debug.Log("neurons " + networkNeurons);

        RecreateNetwork();

        weights = nsm.weights;
        biases = nsm.biases;
        //Debug.Log("weights " + weights);
        //Debug.Log("biases " + biases);
    }


    //TEST ONLY
    public void SaveNetwork()
    {
        //Save button triggers that
        FindObjectOfType<NetworkSaveManager>().SaveToFile("TEST STRING", filename);
    }
    public void RecreateNetwork()
    {
        for (int i = 0; i < networkLayers + 1; i++)
        {
            Matrix<float> newHiddenLayer = Matrix<float>.Build.Dense(1, networkNeurons);
            hiddenLayers.Add(newHiddenLayer);

            if (i == 0)
            {
                Matrix<float> inputToHidden = Matrix<float>.Build.Dense(numberOfInputs, networkNeurons);
                weights.Add(inputToHidden);
            }
            Matrix<float> hiddenToHidden = Matrix<float>.Build.Dense(networkNeurons, networkNeurons);
            weights.Add(hiddenToHidden);
        }
        Matrix<float> outputWeight = Matrix<float>.Build.Dense(networkNeurons, numberOfOutputs);
        weights.Add(outputWeight);
        biases.Add(Random.Range(-1f, 1f));

    }
    /*
    public void CopyWeights()
    {
        for (int i = 0; i < weights.Count; i++)
        {
            for (int x = 0; x < weights[i].RowCount; x++)
            {
                for (int y = 0; y < weights[i].ColumnCount; y++)
                {
                    weights[i][x, y] = Random.Range(-1f, 1f);
                }
            }
        }
    }*/

    private void GetInputFromSensors()
    {
        Vector3 a = (transform.forward + transform.forward + transform.right);
        Vector3 b = (transform.forward + transform.right + transform.right); //test
        Vector3 c = (transform.forward);
        Vector3 d = (transform.forward - transform.right - transform.right); //test;
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
            if (hit.collider.tag == "NotRoad")
            {
                float sensorValue = hit.distance / maxDistance;
                Debug.DrawLine(ray.origin, hit.point, Color.red);
                return sensorValue;
            }
            return 1f;
        }
        return 1f; //1 cuz if not detected then it's max range which means 1??? will it work?
    }
    public (float, float) RunNetwork(float a, float b, float c, float d, float e)
    {
        inputLayer[0, 0] = a;
        inputLayer[0, 1] = b;
        inputLayer[0, 2] = c;
        inputLayer[0, 3] = d;
        inputLayer[0, 4] = e;

        inputLayer = inputLayer.PointwiseTanh();

        hiddenLayers[0] = ((inputLayer * weights[0] + biases[0]).PointwiseTanh());

        for (int i = 1; i < hiddenLayers.Count; i++)
        {
            hiddenLayers[i] = ((hiddenLayers[i - 1] * weights[i] + biases[i]).PointwiseTanh());
        }

        outputLayer = ((hiddenLayers[hiddenLayers.Count - 1] * weights[weights.Count - 1]) + biases[biases.Count - 1]).PointwiseTanh();

        return (Sigmoid(outputLayer[0, 0]), (float)Math.Tanh(outputLayer[0, 1]));
    }
    private float Sigmoid(float s)
    {
        return (1 / (1 + Mathf.Exp(-s)));
    }

    private void GetNetworkOutput()
    {
        (acceleration, turnAngle) = RunNetwork(aSensor, bSensor, cSensor, dSensor, eSensor);
    }

    public void MoveVehicle(float acc, float turn)
    {
        input = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, acc * maxVehicleAcceleration), movementSmoothing);
        input = transform.TransformDirection(input);
        transform.position += input;
        transform.eulerAngles += new Vector3(0, (turn * maxVehicleTurnAngle) * movementSmoothing, 0);
    }


    /* 
    private void CheckForRoad()
    {
        if (!IsOnRoad())
        {
            //Slow down
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

    */ 
}
