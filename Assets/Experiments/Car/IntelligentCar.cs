using UnityEngine;

[RequireComponent(typeof(Car))]
public class IntelligentCar : MonoBehaviour
{
  [SerializeField] private RaycastSensor[] m_Sensors;
  private Car m_Car;
  // Tracking fitness
  private double m_GainedFitness;
  private int m_LastWaypointId;
  private float m_Timer;
  private int m_Hits;

  private void Awake()
  {
    m_Car = GetComponent<Car>();
  }

  private void Update()
  {
    m_Timer += Time.deltaTime;
  }

  private void FixedUpdate()
  {
    var signals = Percept();
  }

  private double[] Percept()
  {
    var signals = new double[m_Sensors.Length];

    for (var i = 0; i < m_Sensors.Length; i++) {
      var s = m_Sensors[i];
      signals[i] = s.Test();
    }

    return signals;
  }

  private void OnTriggerEnter(Collider collider) {}

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.collider.CompareTag("Wall")) {
      m_Hits++;
    }
  }
}