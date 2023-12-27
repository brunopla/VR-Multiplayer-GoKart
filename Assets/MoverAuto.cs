using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoverAuto : MonoBehaviour
{
    public Transform steeringWheelTransform;
    public Transform vehicleTransform;
    public float maxSpeed = 10f;
    public float rotationSpeed = 5f;
    private float currentSpeed = 0f;
    public bool canMove = true;

    private void FixedUpdate()
    {
        if (canMove)
        {
            HandleMovement();
            if (Input.GetKey(KeyCode.Space))
            {
                Accelerate();
            }
        }
    }

    private void HandleMovement()
    {
        // Rotate the vehicle based on the steering wheel's rotation
        float steeringAngle = steeringWheelTransform.localEulerAngles.y > 180 ?
            steeringWheelTransform.localEulerAngles.y - 360 :
            steeringWheelTransform.localEulerAngles.y;

        if (steeringAngle < 0)
        {
            // Turn left
            vehicleTransform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        else if (steeringAngle > 0)
        {
            // Turn right
            vehicleTransform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        // Move the vehicle
        Vector3 movement = vehicleTransform.forward * currentSpeed * Time.fixedDeltaTime;
        vehicleTransform.Translate(movement, Space.World);
    }

    private void Accelerate()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += maxSpeed * Time.deltaTime;
        }
    }

}
