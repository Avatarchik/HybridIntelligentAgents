using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Learning/GymParameters")]
public class GymParameters : ScriptableObject
{
  [SerializeField] private int m_PopulationSize;
  [SerializeField] private NeatParameters[] m_NeatModules;
  [SerializeField] private QLearningParameters[] m_QLearningModules;
  [SerializeField] private SarsaParameters[] m_SarsaModules;
  [SerializeField] private DynaQParameters[] m_DynaQModules;

  private bool m_FirstDeserialization = true;
  private int[] m_ArrayLengths;

  public int populationSize
  {
    get { return m_PopulationSize; }
  }

  public IList<LearningParameters> learningModules
  {
    get
    {
      for (var i = 0; i < m_NeatModules.Length; i++) {
        m_NeatModules[i].populationSize = m_PopulationSize;
      }

      var modules = new List<LearningParameters>();
      modules.AddRange(m_NeatModules);
      modules.AddRange(m_QLearningModules);
      modules.AddRange(m_SarsaModules);
      modules.AddRange(m_DynaQModules);
      return modules;
    }
  }

  private void OnValidate()
  {
    if (m_FirstDeserialization || m_ArrayLengths.Length != 4) {
      m_ArrayLengths = new[] {
        m_NeatModules.Length,
        m_QLearningModules.Length,
        m_SarsaModules.Length,
        m_DynaQModules.Length
      };

      m_FirstDeserialization = false;
    } else {
      // neat
      if (m_NeatModules.Length != m_ArrayLengths[0]) {
        if (m_NeatModules.Length > m_ArrayLengths[0]) {
          for (var i = m_ArrayLengths[0]; i < m_NeatModules.Length; i++) {
            m_NeatModules[i] = new NeatParameters();
          }
        }

        m_ArrayLengths[0] = m_NeatModules.Length;
      }

      // q learning
      if (m_QLearningModules.Length != m_ArrayLengths[1]) {
        if (m_QLearningModules.Length > m_ArrayLengths[1]) {
          for (var i = m_ArrayLengths[1]; i < m_QLearningModules.Length; i++) {
            m_QLearningModules[i] = new QLearningParameters();
          }
        }

        m_ArrayLengths[1] = m_QLearningModules.Length;
      }

      // sarsa
      if (m_SarsaModules.Length != m_ArrayLengths[2]) {
        if (m_SarsaModules.Length > m_ArrayLengths[2]) {
          for (var i = m_ArrayLengths[2]; i < m_SarsaModules.Length; i++) {
            m_SarsaModules[i] = new SarsaParameters();
          }
        }

        m_ArrayLengths[2] = m_SarsaModules.Length;
      }

      // dyna
      if (m_DynaQModules.Length != m_ArrayLengths[3]) {
        if (m_DynaQModules.Length > m_ArrayLengths[3]) {
          for (var i = m_ArrayLengths[3]; i < m_DynaQModules.Length; i++) {
            m_DynaQModules[i] = new DynaQParameters();
          }
        }

        m_ArrayLengths[3] = m_DynaQModules.Length;
      }
    }
  }
}