using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using System;

using Random = UnityEngine.Random;

public class NeuralNetwork : MonoBehaviour
{
    [HideInInspector]
    public Matrix<float> inputLayer = Matrix<float>.Build.Dense(1, 5); //try sparse instead of dense and measure performance
    public Matrix<float> outputLayer = Matrix<float>.Build.Dense(1, 2);
    [HideInInspector]
    public List<Matrix<float>> hiddenLayers = new List<Matrix<float>>();
    public List<Matrix<float>> weights = new List<Matrix<float>>();
    [HideInInspector]
    public List<float> biases = new List<float>();
    [HideInInspector]
    public float fitnessScore;
    [HideInInspector]
    public int numberOfInputs = 5;
    [HideInInspector]
    public int numberOfOutputs = 2;

    public void Initialise(int hiddenLayerCount, int hiddenNeuronCount)
    {
        ClearNetwork();
        BuildNetwork(hiddenLayerCount, hiddenNeuronCount);
        RandomizeWeights();
    }

    public void ClearNetwork()
    {
        ClearLayers();
        weights.Clear();
        biases.Clear();
    }

    public void ClearLayers()
    {
        inputLayer.Clear();
        hiddenLayers.Clear();
        outputLayer.Clear();
    }

    public void BuildNetwork(int hiddenLayerCount, int hiddenNeuronCount)
    {
        for (int i = 0; i < hiddenLayerCount + 1; i++)
        {
            Matrix<float> newHiddenLayer = Matrix<float>.Build.Dense(1, hiddenNeuronCount);
            hiddenLayers.Add(newHiddenLayer);
            biases.Add(Random.Range(-1f, 1f));

            if (i == 0)
            {
                Matrix<float> inputToHidden = Matrix<float>.Build.Dense(numberOfInputs, hiddenNeuronCount);
                weights.Add(inputToHidden);
            }
            Matrix<float> hiddenToHidden = Matrix<float>.Build.Dense(hiddenNeuronCount, hiddenNeuronCount);
            weights.Add(hiddenToHidden);
        }
        Matrix<float> outputWeight = Matrix<float>.Build.Dense(hiddenNeuronCount, numberOfOutputs);
        weights.Add(outputWeight);
        biases.Add(Random.Range(-1f, 1f));
    }

    public void RandomizeWeights()
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
    }

    public NeuralNetwork InitializeNetworkCopy(int hiddenLayerCount, int hiddenNeuronCount)
    {
        NeuralNetwork newNetwork = new NeuralNetwork();

        List<Matrix<float>> newWeights = new List<Matrix<float>>();

        for (int i = 0; i < this.weights.Count; i++)
        {
            Matrix<float> currentWeight = Matrix<float>.Build.Dense(weights[i].RowCount, weights[i].ColumnCount);

            for (int x = 0; x < currentWeight.RowCount; x++)
            {
                for (int y = 0; y < currentWeight.ColumnCount; y++)
                {
                    currentWeight[x, y] = weights[i][x, y];
                }
            }
            newWeights.Add(currentWeight);
        }
        List<float> newBiases = new List<float>();

        newBiases.AddRange(biases);

        newNetwork.weights = newWeights;
        newNetwork.biases = newBiases;

        newNetwork.InitialiseHiddenLayers(hiddenLayerCount, hiddenNeuronCount);

        return newNetwork;
    }

    public void InitialiseHiddenLayers(int hiddenLayerCount, int hiddenNeuronCount)
    {
        ClearLayers();

        for (int i = 0; i < hiddenLayerCount + 1; i++)
        {
            Matrix<float> newHiddenLayer = Matrix<float>.Build.Dense(1, hiddenNeuronCount);
            hiddenLayers.Add(newHiddenLayer);
        }
    }

    public (float, float) RunNetwork(float a, float b, float c, float d, float e)
    {
        inputLayer[0, 0] = a;
        inputLayer[0, 1] = b;
        inputLayer[0, 2] = c;
        inputLayer[0, 3] = d;
        inputLayer[0, 4] = e;
        //SPRAWDZ CZY ZNORMALIZOWANE WARTOSCI SENSORÓW SERIO SA W PRZEDZIALE 0 1, JAK NIE TO PODZIEL PRZEZ ICH DLUGOSC I ES


        //Activation function for value range of <-1,1>
        inputLayer = inputLayer.PointwiseTanh();

        hiddenLayers[0] = ((inputLayer * weights[0] + biases[0]).PointwiseTanh());

        for (int i = 1; i < hiddenLayers.Count; i++)
        {
            hiddenLayers[i] = ((hiddenLayers[i - 1] * weights[i] + biases[i]).PointwiseTanh());
        }

        outputLayer = ((hiddenLayers[hiddenLayers.Count - 1] * weights[weights.Count - 1]) + biases[biases.Count - 1]).PointwiseTanh();

        //First value is acceleration in range between 0 and 1. Second value is "turn angle" in range between -1 and 1
        return (Sigmoid(outputLayer[0, 0]), (float)Math.Tanh(outputLayer[0, 1]));
    }

    
    private float Sigmoid (float s)
    {
        return (1 / (1 + Mathf.Exp(-s)));
    }
}
