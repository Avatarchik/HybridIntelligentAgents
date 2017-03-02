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
}

[Serializable]
public class QLearningParameters : ReinforcementLearningParameters {}

[Serializable]
public class SarsaParameters : ReinforcementLearningParameters {}

[Serializable]
public class DynaQParameters : ReinforcementLearningParameters
{
  [SerializeField] private int m_N;
}