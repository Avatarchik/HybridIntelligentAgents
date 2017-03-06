using System.Collections.Generic;
using Brain.Neat;
using UnityEngine;

public class Gym : MonoBehaviour
{
  [SerializeField] private GymParameters m_Parameters;
  [SerializeField] private IntelligentAgent m_AgentPrefab;

  // Learning
  private IList<IntelligentAgent> m_SpawnedAgents;
  private IList<NeuroEvolution> m_NeuroEvolutions;
  // bodies[module][agentIndex]
  private IList<IList<IBody>> m_Bodies;

  // Termination
  [SerializeField] private float m_TrainingDuration;

  // Time
  private float m_TimeLeft;
  private float m_TimeAccumulation;
  private int m_Frames;
  private readonly float m_UpdateInterval = 12;

  private void Start()
  {
    m_SpawnedAgents = new List<IntelligentAgent>();
    m_NeuroEvolutions = new List<NeuroEvolution>();
    m_Bodies = new List<IList<IBody>>();
  }

  public void StartLearning()
  {
    SpawnAgents();

    for (var i = 0; i < m_Parameters.learningModules.Count; i++) {
      var m = m_Parameters.learningModules[i];
      if (m is NeatParameters) {
        var neat = ((NeatParameters) m).ToNeat();
        m_NeuroEvolutions.Add(new NeuroEvolution(neat));
        m_Bodies.Add(new List<IBody>());
      }
    }

    for (var i = 0; i < m_Parameters.populationSize; i++) {
      var agent = m_SpawnedAgents[i];

      agent.learningMethods = new List<ILearningMethod>();

      // setup learning methods
      /*
      for (var j = 0; j < m_NeuroEvolutions.Count; j++) {
        var ne = m_NeuroEvolutions[j];
        var neatMethod = new NeatLearningMethod(ne.Neat.InputCount, ne.Neat.OutputCount, ne.Neat.MaxFitness);
        agent.learningMethods.Add(neatMethod);

        m_Bodies[j].Add(neatMethod.body);
      }
      */

      var neatModuleIndex = 0;
      for (var j = 0; j < m_Parameters.learningModules.Count; j++) {
        var m = m_Parameters.learningModules[j];

        if (m is NeatParameters) {
          var p = (NeatParameters) m;
          var neat = p.ToNeat();
          var method = new NeatLearningMethod(neat.InputCount, neat.OutputCount, neat.MaxFitness);
          agent.learningMethods.Add(method);

          m_Bodies[neatModuleIndex++].Add(method.body);
        } else 
        if (m is QLearningParameters) {
          var p = (QLearningParameters) m;
          agent.learningMethods.Add(new QLearning(p.stateCount, p.actionCount, p.explorationPolicy, p.learningRate,
            p.discountFactor, p.initializeRandom));
        } else if (m is DynaQParameters) {
          var p = (DynaQParameters) m;
          agent.learningMethods.Add(new DynaQ(p.stateCount, p.actionCount, p.explorationPolicy, p.n, p.learningRate,
            p.discountFactor, p.initializeRandom));
        } else if (m is SarsaParameters) {
          var p = (SarsaParameters) m;
          agent.learningMethods.Add(new Sarsa(p.stateCount, p.actionCount, p.explorationPolicy, p.learningRate,
            p.discountFactor, p.initializeRandom));
        }
      }
    }

    for (var i = 0; i < m_NeuroEvolutions.Count; i++) {
      m_NeuroEvolutions[i].Begin(m_Bodies[i]);
    }

    for (var i = 0; i < m_Parameters.populationSize; i++) {
      m_SpawnedAgents[i].OnSpawn();
    }

    Invoke("StopLearning", m_TrainingDuration);
  }

  private void SpawnAgents()
  {
    for (var i = 0; i < m_Parameters.populationSize; i++) {
      var agent = m_AgentPrefab.Spawn();
      m_SpawnedAgents.Add(agent);
    }
  }

  private void RecycleAgents()
  {
    for (var i = 0; i < m_Parameters.populationSize; i++) {
      m_SpawnedAgents[i].Recycle();
    }

    m_SpawnedAgents.Clear();
  }

  public void StopLearning()
  {
    // TODO: remove this
    for (var i = 0; i < m_Parameters.populationSize; i++) {
      m_SpawnedAgents[i].OnRecycle();
    }

    RecycleAgents();
  }

  public void Epoch()
  {
    for (var i = 0; i < m_NeuroEvolutions.Count; i++) {
      var champ = m_NeuroEvolutions[i].Epoch();
      Debug.Log("champ fitness: " + champ.CalculateRawFitness());
    }
  }

  private void Update()
  {
    m_TimeLeft -= Time.deltaTime;
    m_TimeAccumulation += Time.timeScale / Time.deltaTime;
    m_Frames++;

    if (m_TimeLeft <= 0.0) {
      var fps = m_TimeAccumulation / m_Frames;
      m_TimeLeft = m_UpdateInterval;
      m_TimeAccumulation = 0.0f;
      m_Frames = 0;

      if (fps < 10) {
        Time.timeScale = Time.timeScale - 1.0f;
        Debug.Log("time scale change " + Time.timeScale);
      }
    }

    var finishCount = 0;
    for (var i = 0; i < m_SpawnedAgents.Count; i++) {
      if (m_SpawnedAgents[i].finished) {
        finishCount++;
      }
    }

    // if all finished
    if (finishCount == m_SpawnedAgents.Count) {
      Epoch();
    }
  }
}