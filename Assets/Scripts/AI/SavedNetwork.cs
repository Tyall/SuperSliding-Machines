using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedNetwork 
{
    public int networkLayers;
    public int networkNeurons;

    public float fitness;
    public float time;
    public int generation;
    public int individual;
    

    public List<float[,]> weights;
    public List<float> biases;
}
