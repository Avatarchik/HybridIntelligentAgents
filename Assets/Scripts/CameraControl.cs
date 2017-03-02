using UnityEngine;

public class CameraControl : MonoBehaviour
{
  [SerializeField] private Transform m_Target;
  [SerializeField] private Vector2 m_MoveMin;
  [SerializeField] private Vector2 m_MoveMax;
  private Vector3 m_LastMousePosition;
  private float m_Distance;
  [SerializeField] private float m_ScrollMultiplier;

  private void Start()
  {
    m_Distance = 40.0f;
  }

  private void Update()
  {
    if (Input.GetMouseButtonDown(0)) {
      m_LastMousePosition = Input.mousePosition;
    } else if (Input.GetMouseButton(0)) {
      var pos = Input.mousePosition;
      var md = (m_LastMousePosition - pos) * m_Distance * 0.3f * Time.unscaledDeltaTime;
      m_LastMousePosition = pos;

      var t = m_Target.position;
      t.x = Mathf.Clamp(t.x + md.x, m_MoveMin.x, m_MoveMax.x);
      t.z = Mathf.Clamp(t.z + md.y, m_MoveMin.y, m_MoveMax.y);
      m_Target.position = t;
    }

    var scroll = m_Distance + Input.mouseScrollDelta.y * m_ScrollMultiplier * -m_Distance * Time.unscaledDeltaTime;
    m_Distance = Mathf.Clamp(scroll, 5.0f, 40.0f);

    transform.position = m_Target.position + m_Target.forward * m_Distance;
    transform.LookAt(m_Target, Vector3.up);
  }
}