using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chart : MonoBehaviour
{
  [SerializeField] private UILineRenderer m_Line;
  [SerializeField] private RectTransform m_ChartContent;
  private IList<float> m_Values;

  private void Awake()
  {
    m_Values = new List<float>();
  }

  public void AddValue(float value)
  {
    if (value <= 0.0f) {
      return;
    }

    m_Values.Add(value);
    UpdateUI();
  }

  public void Clear()
  {
    m_Values.Clear();
    UpdateUI();
  }

  private void UpdateUI()
  {
    if (m_Values.Count < 2) {
      return;
    }

    var maxValue = m_Values.Max();
    var points = new Vector2[m_Values.Count];
    var s = m_ChartContent.rect.size;

    for (var i = 0; i < points.Length; i++) {
      var x = (i / (float) (points.Length - 1)) * s.x;
      points[i] = new Vector2(x, (m_Values[i] / maxValue) * s.y);
    }

    m_Line.Points = points;
  }
}