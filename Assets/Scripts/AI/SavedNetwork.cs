using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedNetwork 
{
    public int networkLayers;
    public int networkNeurons;

    public List<float[,]> weights;
    public List<float> biases;
}
