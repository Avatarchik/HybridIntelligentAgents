using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
  [SerializeField] private List<AxleInfo> m_AxleInfos;
  [SerializeField] private float m_MaxMotorTorque;
  [SerializeField] private float m_MaxSteeringAngle;
  [SerializeField] private float m_Acceleration;
  [SerializeField] private float m_Steer;

  public Rigidbody rigidbody { get; private set; }

  public float speed
  {
    get { return rigidbody.velocity.magnitude; }
  }

  private void Awake()
  {
    rigidbody = GetComponent<Rigidbody>();
  }

  public void Control(float acceleration, float steer)
  {
    m_Acceleration = acceleration;
    m_Steer = Mathf.Clamp(steer, -1.0f, 1.0f);
  }

  private void FixedUpdate()
  {
    var motor = m_MaxMotorTorque * m_Acceleration;
    var steering = m_MaxSteeringAngle * m_Steer;

    foreach (var axleInfo in m_AxleInfos) {
      if (axleInfo.steering) {
        axleInfo.leftWheel.steerAngle = steering;
        axleInfo.rightWheel.steerAngle = steering;
      }
      if (axleInfo.motor) {
        axleInfo.leftWheel.motorTorque = motor;
        axleInfo.rightWheel.motorTorque = motor;
      }

      ApplyLocalPositionToVisuals(axleInfo.leftWheel);
      ApplyLocalPositionToVisuals(axleInfo.rightWheel);
    }
  }

  private void ApplyLocalPositionToVisuals(WheelCollider collider)
  {
    if (collider.transform.childCount == 0) {
      return;
    }

    var visualWheel = collider.transform.GetChild(0);

    Vector3 position;
    Quaternion rotation;
    collider.GetWorldPose(out position, out rotation);

    visualWheel.transform.position = position;
    visualWheel.transform.rotation = rotation;
  }

  [Serializable]
  public class AxleInfo
  {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
  }
}