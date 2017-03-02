using UnityEngine;

[RequireComponent(typeof(Car))]
public class ControlledCar : MonoBehaviour
{
  private Car _car;

  private void Start()
  {
    _car = GetComponent<Car>();
  }

  private void FixedUpdate()
  {
    var v = Input.GetAxis("Vertical");
    var h = Input.GetAxis("Horizontal");

    _car.Control(v, h);
  }
}