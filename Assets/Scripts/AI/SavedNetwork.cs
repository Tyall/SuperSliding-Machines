using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedNetwork 
{
    public int networkLayers;
    public int networkNeurons;

    /*public int Generation;
    public int Individual;
    public string Reason;
    public float Fitness;*/

    public List<float[,]> weights;
    public List<float> biases;
}
