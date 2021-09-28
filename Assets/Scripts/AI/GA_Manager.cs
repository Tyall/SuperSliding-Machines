using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

public class GA_Manager : MonoBehaviour
{
    public AI_Manager manager;

    [Header("Algorithm Parameters")]
    public int initialPopulationSize = 40;
    public int bestIndividualsSelected = 8;
    public int numberToCrossover;
    [Range(0.0f, 1.0f)]
    public float mutationRate = 0.05f;

    private List<int> genePool = new List<int>();

    private NeuralNetwork[] population;
    private int naturallySelected;

    [Header("Evolution statistics")]
    public int currentGeneration;
    public int currentIndividual;

    private void Start()
    {
        CreatePopulation();
    }

    private void CreatePopulation()
    {
        population = new NeuralNetwork[initialPopulationSize];
        FillPopulationWithRandomValues(population, 0);
        SetToNextIndividual();
    }

    private void FillPopulationWithRandomValues(NeuralNetwork[] population, int startingIndex)
    {
        while (startingIndex < initialPopulationSize)
        {
            /*NeuralNetwork[] net = gameObject.GetComponents<NeuralNetwork>();
            foreach (NeuralNetwork n in net)
            {
                Destroy(n);
            }
            
            population[startingIndex] = gameObject.AddComponent<NeuralNetwork>();*/
                       population[startingIndex] = new NeuralNetwork();
            population[startingIndex].Initialise(manager.networkLayers, manager.networkNeurons);
            startingIndex++;
        }
    }

    private void SetToNextIndividual()
    {
        manager.ResetStatsAndSetNetwork(population[currentIndividual]);
    }

    public void Death(float fitnessScore, NeuralNetwork network)
    {
        if (currentIndividual < population.Length - 1)
        {
            population[currentIndividual].fitnessScore = fitnessScore;
            currentIndividual++;
            SetToNextIndividual();
        }
        else
        {
            CreateNewGeneration();
        }
    }

    private void CreateNewGeneration()
    {
        genePool.Clear();
        currentGeneration++;
        naturallySelected = 0;

        SortPopulation();

        NeuralNetwork[] newPopulation = PickBestPopulation();

        Crossover(newPopulation);
        Mutate(newPopulation);

        FillPopulationWithRandomValues(newPopulation, naturallySelected);

        population = newPopulation;
        currentIndividual = 0;

        SetToNextIndividual();
    }
    /*
    private void SortPopulation()
    {
        System.Array.Sort(population, delegate(Mnet))
        for (int i = 0; i < population.Length; i++)
        {
            for (int j = i; j < population.Length; j++)
            {
                if (population[i].fitnessScore < population[j].fitnessScore)
                {
                    NeuralNetwork temp = population[i];
                    population[i] = population[j];
                    population[j] = temp;
                }
            }
        }
    }*/
    // Sort from highest [0] to lowest [InitialPopulation] fitness
    void SortPopulation() ///?
    {
        System.Array.Sort(population, delegate (NeuralNetwork x, NeuralNetwork y) { return y.fitnessScore.CompareTo(x.fitnessScore); });
    }
    private NeuralNetwork[] PickBestPopulation()
    {
        NeuralNetwork[] newPopulation = new NeuralNetwork[initialPopulationSize];

        for (int i = 0; i < bestIndividualsSelected; i++)
        {
            newPopulation[naturallySelected] = population[i].InitializeNetworkCopy(manager.networkLayers, manager.networkNeurons);
            newPopulation[naturallySelected].fitnessScore = 0;
            naturallySelected++;

            int scoreThreshold = Mathf.RoundToInt(population[i].fitnessScore * 10); ;

            for (int j = 0; j < scoreThreshold + 1; j++)
            {
                genePool.Add(i);
            }
        }
        return newPopulation;
    }

    private void Crossover(NeuralNetwork[] newPopulation)
    {
        for (int i = 0; i < numberToCrossover; i += 2)
        {
            int indexA = i;
            int indexB = i + 1;

            if (genePool.Count >= 1)
            {
                for (int j = 0; j < 100; j++)
                {
                    indexA = genePool[Random.Range(0, genePool.Count)];
                    indexB = genePool[Random.Range(0, genePool.Count)];

                    if (indexA != indexB)
                    {
                        break;
                    }
                }
            }

            NeuralNetwork child1 = new NeuralNetwork();
            NeuralNetwork child2 = new NeuralNetwork();

            child1.Initialise(manager.networkLayers, manager.networkNeurons);
            child2.Initialise(manager.networkLayers, manager.networkNeurons);

            child1.fitnessScore = 0;
            child2.fitnessScore = 0;

            for (int k = 0; k < child1.weights.Count; k++)
            {
                if (Random.Range(0.0f, 1.0f) < 0.5f)
                {
                    child1.weights[k] = population[indexA].weights[k];
                    child2.weights[k] = population[indexB].weights[k];
                }
                else
                {
                    child2.weights[k] = population[indexA].weights[k];
                    child1.weights[k] = population[indexB].weights[k];
                }
            }

            for (int l = 0; l < child1.biases.Count; l++)
            {
                if (Random.Range(0.0f, 1.0f) < 0.5f)
                {
                    child1.biases[l] = population[indexA].biases[l];
                    child2.biases[l] = population[indexB].biases[l];
                }
                else
                {
                    child2.biases[l] = population[indexA].biases[l];
                    child1.biases[l] = population[indexB].biases[l];
                }
            }

            newPopulation[naturallySelected] = child1;
            naturallySelected++;

            newPopulation[naturallySelected] = child2;
            naturallySelected++;
            
        }
    }

    private void Mutate(NeuralNetwork[] population)
    {
        for (int i = 0; i < naturallySelected; i++)
        {
            for (int j = 0; j < population[i].weights.Count; j++)
            {
                if (Random.Range(0.0f, 1.0f) < mutationRate)
                {
                    population[i].weights[j] = MutateWeights(population[i].weights[j]);
                }
            }
        }
    }

    Matrix<float> MutateWeights(Matrix<float> matrix)
    {
        int mutationLevel = Random.Range(1, matrix.RowCount * matrix.ColumnCount);

        Matrix<float> operationalMatrix = matrix;

        for (int i = 0; i < mutationLevel; i++)
        {
            int randomRow = Random.Range(0, operationalMatrix.RowCount);
            int randomColumn = Random.Range(0, operationalMatrix.ColumnCount);

            operationalMatrix[randomRow, randomColumn] = Mathf.Clamp(operationalMatrix[randomRow, randomColumn] + Random.Range(-1, 1f), -1f, 1f);
        }
        return operationalMatrix;
    }
}
