using System;
using UnityEngine;

[Serializable]
public abstract class ReinforcementLearningParameters : LearningParameters
{
  public enum ExplorationPolicy
  {
    Boltzman,
    EpsilonGreedy
  }

  [SerializeField] private int m_StateCount;
  [SerializeField] private int m_ActionCount;
  [SerializeField] private ExplorationPolicy m_ExplorationPolicy = ExplorationPolicy.EpsilonGreedy;
  [SerializeField] private double m_LearningRate = 0.1;
  [SerializeField] private double m_DiscountFactor = 0.9;
  [SerializeField] private bool m_InitializeRandom;

  public int stateCount { get { return m_StateCount; } }
  public int actionCount { get { return m_ActionCount; } }
  public double learningRate { get { return m_LearningRate; } }
  public double discountFactor { get { return m_DiscountFactor; } }
  public bool initializeRandom { get { return m_InitializeRandom; } }

  public IExplorationPolicy explorationPolicy
  {
    get
    {
      // TODO: set parameters
      switch (m_ExplorationPolicy) {
          case ExplorationPolicy.Boltzman:
          return new BoltzmannExplorationPolicy();
          case ExplorationPolicy.EpsilonGreedy:
          return new EpsilonGreedyExplorationPolicy();
      }
      return null;
    }
  }
}

[Serializable]
public class QLearningParameters : ReinforcementLearningParameters {}

[Serializable]
public class SarsaParameters : ReinforcementLearningParameters {}

[Serializable]
public class DynaQParameters : ReinforcementLearningParameters
{
  [SerializeField] private int m_N;

  public int n { get { return m_N; } }
}