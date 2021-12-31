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
        
        string chosenJson;
        var jsonList = new List<string>();
        jsonList = LoadFromFile(filename);

        int difficulty = PlayerProfile.difficulty;

        //DEBUG ONLY - CUZ LOADING PLAYER DOESNT WORK IN DEBUG
        difficulty = 2;
        //DEBUG END

        int chosenLine = GetRandomLine(difficulty, jsonList.Count);
        Debug.Log("Chosen line is " + chosenLine + " difficulty " +difficulty);
        chosenJson = jsonList[chosenLine].ToString();
        
        SavedNetwork saved = JsonConvert.DeserializeObject<SavedNetwork>(chosenJson);

        this.networkLayers = saved.networkLayers;
        this.networkNeurons = saved.networkNeurons;
        this.weights = WeightArrayToMatrix(saved.weights);
        this.biases = saved.biases;


        Debug.Log("Player set AI Difficulty to " + difficulty + ". Loaded json from line " + chosenLine + " consisting data of " + chosenJson);

    }

    int GetRandomLine(int difficulty, int listSize)
    {
        System.Random rnd = new System.Random();
        switch (difficulty)
        {
            case 1:
                return rnd.Next(0, 30);
            case 2:
                return rnd.Next(31, 60);
            case 3:
                return rnd.Next(61, listSize);
        }
        return -1;
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

    public void Save(NeuralNetwork network, string filename, int nLayers, int nNeurons, string diff, float fit, float time, float indTime, float indTravelDistance, float indAvgSpeed, float indDistanceScore, float indSpeedScore, float indRacingLineScore, int gen, int ind)
    {
        SavedNetwork saved = new SavedNetwork();

        saved.networkLayers = nLayers + 2;
        saved.networkNeurons = nNeurons;
        
        saved.weights = WeightMatrixToArray(network.weights);
        saved.biases = network.biases;

        saved.difficulty = diff;
        saved.fitness = fit;
        saved.time = time;
        saved.individualTime = indTime;
        saved.individualTraveledDistance = indTravelDistance;
        saved.individualAvgSpeed = indAvgSpeed;
        saved.individualDistanceScore = indDistanceScore;
        saved.individualSpeedScore = indSpeedScore;
        saved.individualRacingLineScore = indRacingLineScore;
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

    public List<string> LoadFromFile(string filename)
    {
        
        var jsonList = new List<string>();

        string path = "Assets/Resources/SavedModels/" + filename;

        Debug.Log("Trying to load file of path " + path);

        StreamReader reader = new StreamReader(path);

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            jsonList.Add(line);
        }

        reader.Close();
        

        return jsonList;
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
