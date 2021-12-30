using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using Newtonsoft.Json;
using System.IO;


public class NetworkSaveManager : MonoBehaviour
{
    //Rebuild it so it'll work at runtime

    public int networkLayers;
    public int networkNeurons;

    public List<Matrix<float>> weights = new List<Matrix<float>>();
    public List<float> biases;

    
    public void Load(string filename)
    {

        string json = LoadFromFile(filename);
        SavedNetwork saved = JsonConvert.DeserializeObject<SavedNetwork>(json);

        this.networkLayers = saved.networkLayers;
        this.networkNeurons = saved.networkNeurons;
        this.weights = WeightArrayToMatrix(saved.weights);
        this.biases = saved.biases;

        /*Debug.Log("networkLayers " + networkLayers);
        Debug.Log("networkNeurons " + networkNeurons);
        Debug.Log("networkWeights " + weights);
        Debug.Log("networkBiases " + biases);*/

    }

    /*public void DebugSave(NeuralNetwork network, string filename, int nLayers, int nNeurons, float fit, string reason, int gen, int ind)
    {
        SavedNetwork saved = new SavedNetwork();

        saved.networkLayers = nLayers;
        saved.networkNeurons = nNeurons;

        saved.Fitness = fit;
        saved.Reason = reason;
        saved.Generation = gen;
        saved.Individual = ind;
        

        saved.weights = WeightMatrixToArray(network.weights);
        saved.biases = network.biases;

        string json = JsonConvert.SerializeObject(saved);

        SaveToFile(json, filename);
    }*/

    public void Save(NeuralNetwork network, string filename, int nLayers, int nNeurons, string diff, float fit, float time, int gen, int ind)
    {
        SavedNetwork saved = new SavedNetwork();

        saved.networkLayers = nLayers + 2;
        saved.networkNeurons = nNeurons;
        
        saved.weights = WeightMatrixToArray(network.weights);
        saved.biases = network.biases;

        saved.difficulty = diff;
        saved.fitness = fit;
        saved.time = time;
        saved.generation = gen;
        saved.individual = ind;

        string json = JsonConvert.SerializeObject(saved);

        SaveToFile(json, filename);
    }

    private List<float[,]> WeightMatrixToArray(List<Matrix<float>> weights)
    {
        List<float[,]> newWeights = new List<float[,]>();
        foreach(Matrix<float> w in weights)
        {
            newWeights.Add(w.ToArray());
        }
        return newWeights;
    }

    private List<Matrix<float>> WeightArrayToMatrix(List<float[,]> weights)
    {
        List<Matrix<float>> newWeights = new List<Matrix<float>>();
        foreach(float[,] w in weights)
        {
            newWeights.Add(Matrix<float>.Build.DenseOfArray(w));
        }
        return newWeights;
    }

    public string LoadFromFile(string filename)
    {
        string json;

        string path = "Assets/Resources/SavedModels/" + filename;
        StreamReader reader = new StreamReader(path);

        json = reader.ReadToEnd();

        reader.Close();
        Debug.Log("loaded json: " + json);

        return json;
    }

    public void SaveToFile(string json, string filename)
    {
        string path = "Assets/Resources/SavedModels/" + filename;
        Debug.Log("path: " + path);

        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(json);
        writer.Close();

    }

}
