using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInput : MonoBehaviour
{
private CarInputActions carInputActions;
  private void Awake() {
     carInputActions = new CarInputActions();
    carInputActions.Car.Enable();
  }
  public Vector2 GetMovementInput() {
    Vector2 movementInput = carInputActions.Car.Move.ReadValue<Vector2>();
    return movementInput;
  }
}
