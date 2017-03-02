using System.Collections.Generic;
using UnityEngine;

public class Gym : MonoBehaviour
{
  [SerializeField] private GymParameters m_Parameters;

  // Time
  private float m_TimeLeft;
  private float m_TimeAccumulation;
  private int m_Frames;
  private readonly float m_UpdateInterval = 12;

  private void Start() {}

  public void StartLearning() {}

  public void StopLearning() {}

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
  }
}