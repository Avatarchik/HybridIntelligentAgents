using Brain.Neat;

public class NeatLearningMethod : ILearningMethod
{
  private class BodyProxy : IBody
  {
    public double[] Inputs { get; set; }
    public double[] Outputs { get; set; }
    public Organism Organism { get; set; }

    public void Reset()
    {
      Fitness = 0.0;
    }

    public void Activate(double[] outputs)
    {
      Outputs = outputs;
    }

    public bool HasFinished()
    {
      return true;
    }

    public double[] GetInputs()
    {
      return Inputs;
    }

    public int InputCount { get; set; }
    public int OutputCount { get; set; }
    public double Fitness { get; set; }
    public double MaxFitness { get; set; }
  }

  private BodyProxy m_BodyProxy;

  public IBody body { get { return m_BodyProxy;} }

  public int inputCount { get { return m_BodyProxy.InputCount; } }
  public int outputCount { get { return m_BodyProxy.OutputCount; } }
  public double fitness { get { return m_BodyProxy.Fitness; } set { m_BodyProxy.Fitness = value; } }

  public double[] Compute(double[] inputs)
  {
    return m_BodyProxy.Organism.Phenotype.Compute(inputs);
  }

  public NeatLearningMethod(int inputCount, int outputCount, double maxFitness)
  {
    m_BodyProxy = new BodyProxy {
      InputCount = inputCount,
      OutputCount = outputCount,
      MaxFitness = maxFitness
    };
  }
}
