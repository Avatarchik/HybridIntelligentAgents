using System.Collections.Generic;

public interface IBrainPart {}

public class BrainAnatomy
{
  private IList<IBrainPart> m_Parts;

  private BrainAnatomy(IList<IBrainPart> parts)
  {
    m_Parts = parts;
  }

  public class Builder
  {
    private readonly IList<IBrainPart> m_Parts;

    public Builder()
    {
      m_Parts = new List<IBrainPart>();
    }

    public Builder WithNeat(int inputSize, int outputSize)
    {
      m_Parts.Add(new NeatBrainPart());
      return this;
    }

    public Builder WithReinforcementLearning(int stateCount, int actionCount)
    {
      m_Parts.Add(new ReinforcementBrainPart());

      return this;
    }

    public BrainAnatomy Build()
    {
      return new BrainAnatomy(m_Parts);
    }
  }
}