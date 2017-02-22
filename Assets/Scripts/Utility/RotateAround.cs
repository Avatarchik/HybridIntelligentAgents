using UnityEngine;

public class RotateAround : MonoBehaviour
{
  [SerializeField] private Transform m_Pivot;
  [SerializeField] private Vector3 m_Axis;

  private float m_Speed;

  private void Awake()
  {
    m_Speed = m_Axis.magnitude;
  }

  private void Update()
  {
    transform.RotateAround(m_Pivot.position, m_Axis, m_Speed * Time.deltaTime);
  }
}