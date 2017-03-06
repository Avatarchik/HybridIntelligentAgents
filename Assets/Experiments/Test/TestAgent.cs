using System.Collections;
using UnityEngine;

public class TestAgent : IntelligentAgent
{
  private IReinforcementLearning m_QLearning;
  private NeatLearningMethod m_NeatLearning;

  public override void OnSpawn()
  {
    m_QLearning = (IReinforcementLearning)learningMethods[1];
    m_QLearning.Begin(0);

    m_NeatLearning = (NeatLearningMethod) learningMethods[0];

    var result = m_NeatLearning.Compute(new[] {
      1.0,
      -1.0
    });

    m_NeatLearning.fitness = result[0] * result[1];
    Debug.Log("Fitness: " + m_NeatLearning.fitness);

    finished = true;
  }

  public override void OnRecycle()
  {
  }
}