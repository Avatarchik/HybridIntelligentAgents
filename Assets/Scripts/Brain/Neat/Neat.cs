using Brain.Neuro;

namespace Brain.Neat
{
  public class Neat
  {
    public int PopulationSize { get; set; }
    public int InputCount { get; set; }
    public int OutputCount { get; set; }
    public NeuralParameters Neural { get; set; }
    public MutationParameters Mutation { get; set; }
    public SpeciationParameters Speciation { get; set; }
    public ReproductionParameters Reproduction { get; set; }
    public StructureParameters Structure { get; set; }

    public Neat()
    {
      Neural = new NeuralParameters();
      Mutation = new MutationParameters();
      Speciation = new SpeciationParameters();
      Reproduction = new ReproductionParameters();
      Structure = new StructureParameters();
    }

    public class NeuralParameters
    {
      public IActivationFunction ActivationFunction = Neuro.ActivationFunction.Tanh;
      public double MaxActivation = 1.0;
      public double MinActivation = -1.0;
      public double MaxWeight = 1.0;
      public double MinWeight = -1.0;
    }

    public class MutationParameters
    {
      public double ConnectionMutationProbability = 0.05;
      public double NeuralMutationProbability = 0.03;
      public double TotalWeightResetProbability = 0.1;
      public double WeightMutationProbability = 0.8;
    }

    public class SpeciationParameters
    {
      public double CompatibilityThreshold = 3.0;
      public double ImportanceOfAverageWeightDifference = 2.0;
      public double ImportanceOfDisjointGenes = 1.0;
      public bool NormalizeForLargerGenome = false;
      public int StagnantSpeciesClearThreshold = 15;
    }

    public class ReproductionParameters
    {
      public double InterspecialReproductionProbability = 0.001;
      public int MinParents = 1;
      public int MinSpeciesSizeForChampConservation = 5;
      public double ReproductionThreshold = 0.2;
    }

    public class StructureParameters
    {
      public bool AllowRecurrentConnections = false;
      public int BiasNeuronCount = 1;
      public int MemoryResetBeforeTotalReset = 0;
    }
  }
}