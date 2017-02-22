using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (LineRenderer))]
public class RaycastSensor : MonoBehaviour
{
  [SerializeField] private LayerMask m_LayerMask;
  private LineRenderer m_LineRenderer;

  private void Awake()
  {
    m_LineRenderer = GetComponent<LineRenderer>();
    length = m_LineRenderer.GetPosition(1).z;
  }

  public double Test()
  {
    var hit = default(RaycastHit);

    if (Physics.Raycast(transform.position, transform.forward, out hit, length, m_LayerMask)) {
      m_LineRenderer.startColor = Color.red;
      m_LineRenderer.endColor = Color.red;
      m_LineRenderer.SetPosition(1, new Vector3(0.0f, 0.0f, hit.distance));

      return 1.0 - hit.distance / length;
    } else {
      m_LineRenderer.startColor = Color.blue;
      m_LineRenderer.endColor = Color.blue;
      m_LineRenderer.SetPosition(1, new Vector3(0.0f, 0.0f, length));
    }

    return double.NaN;
  }

  public float length { get; set; }
}
