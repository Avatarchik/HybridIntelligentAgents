using UnityEngine;
using UnityEngine.UI;

public class GymUI : MonoBehaviour
{
  [SerializeField] private Gym m_Gym;
  [SerializeField]
  private Text m_GenerationText;
  [SerializeField]
  private Text m_FitnessText;

  public void OnStartLearningClick()
  {
     m_Gym.StartLearning();
  }

  public void OnEvolve()
  {
    //m_GenerationText.text = m_Gym.generation.ToString("N0");
    //m_FitnessText.text = string.Format("{0:0.00}", m_Gym.fitness);
  }

  public void OnChangeSpeed(Slider slider)
  {
    Time.timeScale = slider.value;
  }
}