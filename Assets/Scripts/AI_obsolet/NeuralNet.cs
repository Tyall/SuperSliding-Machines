using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MathNet.Numerics.LinearAlgebra;
using System;

using Random = UnityEngine.Random;

//OgName NeuralNetwork

public class NeuralNet : MonoBehaviour
{
    
    public Matrix<float> inputLayer = Matrix<float>.Build.Dense(1, 3); //1x3 matrix with 3 inputs

    public List<Matrix<float>> hiddenLayers = new List<Matrix<float>>();

    public Matrix<float> outputLayer = Matrix<float>.Build.Dense(1, 2);

    public List<Matrix<float>> weights = new List<Matrix<float>>();

    public List<float> biases = new List<float>();

    public float fitness;

    public void Initialise(int hiddenLayerCount, int hiddenNeuronCount)
    {
        inputLayer.Clear();
        hiddenLayers.Clear();
        outputLayer.Clear();
        weights.Clear();
        biases.Clear();

        for (int i = 0; i < hiddenLayerCount + 1; i++)
        {
            Matrix<float> matrix = Matrix<float>.Build.Dense(1, hiddenNeuronCount);

            hiddenLayers.Add(matrix);

            biases.Add(Random.Range(-1f, 1f));

            //Weights
            if (i == 0)
            {
                Matrix<float> inputToH1 = Matrix<float>.Build.Dense(3, hiddenNeuronCount); //H1 - hidden layer 1, assigning it individually there cuz rows have to match columns to multipliacte input and h1 matrices
                weights.Add(inputToH1);
            }

            Matrix<float> HiddenToHidden = Matrix<float>.Build.Dense(hiddenNeuronCount, hiddenNeuronCount); //rest of the hidden layers
            weights.Add(HiddenToHidden);
        }

        Matrix<float> OutputWeight = Matrix<float>.Build.Dense(hiddenNeuronCount, 2);// A matrix is a hidden layer, B matrix is the output layer
        weights.Add(OutputWeight);
        biases.Add(Random.Range(-1f, 1f));

        RandomizeWeights();
    }

    public NeuralNet InitialiseCopy(int hiddenLayerCount, int hiddenNeuronCount)
    {
        NeuralNet n = new NeuralNet();

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

        n.weights = newWeights;
        n.biases = newBiases;

        n.InitialiseHidden(hiddenLayerCount, hiddenNeuronCount);

        return n;
    }

    public void InitialiseHidden(int hiddenLayerCount, int hiddenNeuronCount)
    {
        inputLayer.Clear();
        hiddenLayers.Clear();
        outputLayer.Clear();

        for (int i = 0; i < hiddenLayerCount + 1; i++)
        {
            Matrix<float> newHiddenLayer = Matrix<float>.Build.Dense(1, hiddenNeuronCount);
            hiddenLayers.Add(newHiddenLayer);
        }
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

    public (float, float) RunNetwork (float a, float b, float c) //This notation allows to return struct of two float variables
    {
        inputLayer[0, 0] = a;
        inputLayer[0, 1] = b;
        inputLayer[0, 2] = c;

        inputLayer = inputLayer.PointwiseTanh(); //Using hyperbolic tangent activation function instead of commonly used Sigmoid since we need end values in range of <-1,1> instead of <0,1>

        hiddenLayers[0] = ((inputLayer * weights[0]) + biases[0]).PointwiseTanh();//Once again first hidden layer needs to be calculated outside of the loop because it's multiplied with input layer

        for (int i = 1; i < hiddenLayers.Count; i++)
        {
            hiddenLayers[i] = ((hiddenLayers[i - 1] * weights[i]) + biases[i]).PointwiseTanh();
        }

        outputLayer = ((hiddenLayers[hiddenLayers.Count - 1] * weights[weights.Count - 1]) + biases[biases.Count - 1]).PointwiseTanh();

        //First output is acceleration, second output is steering
        return (Sigmoid(outputLayer[0,0]),(float)Math.Tanh(outputLayer[0,1]));
        //Sigmoid activates the acceleration since we need values in range of [0,1]
        //Tanh activates the steering since we need values in range of [-1,1]
    }

    private float Sigmoid (float s) //Sigmoid function 
    {
        return (1 / (1 + Mathf.Exp(-s)));
    }
}
