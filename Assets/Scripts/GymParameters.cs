using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Learning/GymParameters")]
public class GymParameters : ScriptableObject
{
  [SerializeField] private NeatParameters[] m_NeatModules;
  [SerializeField] private QLearningParameters[] m_QLearningModules;
  [SerializeField] private SarsaParameters[] m_SarsaModules;
  [SerializeField] private DynaQParameters[] m_DynaQModules;

  public IList<LearningParameters> learningModules
  {
    get
    {
      var modules = new List<LearningParameters>();
      modules.AddRange(m_NeatModules);
      modules.AddRange(m_QLearningModules);
      modules.AddRange(m_SarsaModules);
      modules.AddRange(m_DynaQModules);
      return modules;
    }    
  }
}