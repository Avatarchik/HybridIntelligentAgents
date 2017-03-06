using System;
using Brain.Neat;
using UnityEngine;

[Serializable]
public class NeatParameters : LearningParameters
{
  public enum ActivationFunction
  {
    Tanh,
    Sigmoid,
    ReLU,
    Identity
  }

  [SerializeField] private int m_InputCount;
  [SerializeField] private int m_OutputCount;
  [SerializeField] private double m_MaxFitness;
  [SerializeField] private NeuralParameters m_Neural;
  [SerializeField] private MutationParameters m_Mutation;
  [SerializeField] private SpeciationParameters m_Speciation;
  [SerializeField] private ReproductionParameters m_Reproduction;
  [SerializeField] private StructureParameters m_Structure;

  public NeatParameters()
  {
    m_Neural = new NeuralParameters();
    m_Mutation = new MutationParameters();
    m_Speciation = new SpeciationParameters();
    m_Reproduction = new ReproductionParameters();
    m_Structure = new StructureParameters();
  }

  public int populationSize { get; set; }

  public Neat ToNeat()
  {
    var af = Brain.Neuro.ActivationFunction.Tanh;
    switch (m_Neural.activationFunction) {
      case ActivationFunction.Tanh:
        break;
      case ActivationFunction.Sigmoid:
        af = Brain.Neuro.ActivationFunction.Sigmoid;
        break;
      case ActivationFunction.ReLU:
        af = Brain.Neuro.ActivationFunction.ReLU;
        break;
      case ActivationFunction.Identity:
        af = Brain.Neuro.ActivationFunction.Identity;
        break;
    }

    return new Neat {
      PopulationSize = populationSize,
      InputCount = m_InputCount,
      OutputCount = m_OutputCount,
      MaxFitness = m_MaxFitness,
      Neural = new Neat.NeuralParameters {
        ActivationFunction = af,
        MaxActivation = m_Neural.maxActivation,
        MinActivation = m_Neural.minActivation,
        MaxWeight = m_Neural.maxWeight,
        MinWeight = m_Neural.minWeight
      },
      Mutation = new Neat.MutationParameters {
        ConnectionMutationProbability = m_Mutation.connectionMutationProbability,
        NeuralMutationProbability = m_Mutation.neuralMutationProbability,
        TotalWeightResetProbability = m_Mutation.totalWeightResetProbability,
        WeightMutationProbability = m_Mutation.weightMutationProbability
      },
      Speciation = new Neat.SpeciationParameters {
        CompatibilityThreshold = m_Speciation.compatibilityThreshold,
        ImportanceOfAverageWeightDifference = m_Speciation.importanceOfAverageWeightDifference,
        ImportanceOfDisjointGenes = m_Speciation.importanceOfDisjointGenes,
        NormalizeForLargerGenome = m_Speciation.normalizeForLargerGenome,
        StagnantSpeciesClearThreshold = m_Speciation.stagnantSpeciesClearThreshold
      },
      Reproduction = new Neat.ReproductionParameters {
        InterspecialReproductionProbability = m_Reproduction.interspecialReproductionProbability,
        MinParents = m_Reproduction.minParents,
        MinSpeciesSizeForChampConservation = m_Reproduction.minSpeciesSizeForChampConservation,
        ReproductionThreshold = m_Reproduction.reproductionThreshold
      },
      Structure = new Neat.StructureParameters {
        AllowRecurrentConnections = m_Structure.allowRecurrentConnections,
        BiasNeuronCount = m_Structure.biasNeuronCount,
        MemoryResetBeforeTotalReset = m_Structure.memoryResetBeforeTotalReset
      }
    };
  }

  [Serializable]
  public class NeuralParameters
  {
    public ActivationFunction activationFunction = ActivationFunction.Tanh;
    public double maxActivation = 1.0;
    public double minActivation = -1.0;
    public double maxWeight = 1.0;
    public double minWeight = -1.0;
  }

  [Serializable]
  public class MutationParameters
  {
    public double connectionMutationProbability = 0.05;
    public double neuralMutationProbability = 0.03;
    public double totalWeightResetProbability = 0.1;
    public double weightMutationProbability = 0.8;
  }

  [Serializable]
  public class SpeciationParameters
  {
    public double compatibilityThreshold = 3.0;
    public double importanceOfAverageWeightDifference = 2.0;
    public double importanceOfDisjointGenes = 1.0;
    public bool normalizeForLargerGenome = false;
    public int stagnantSpeciesClearThreshold = 15;
  }

  [Serializable]
  public class ReproductionParameters
  {
    public double interspecialReproductionProbability = 0.001;
    public int minParents = 1;
    public int minSpeciesSizeForChampConservation = 5;
    public double reproductionThreshold = 0.2;
  }

  [Serializable]
  public class StructureParameters
  {
    public bool allowRecurrentConnections = false;
    public int biasNeuronCount = 1;
    public int memoryResetBeforeTotalReset = 0;
  }
}