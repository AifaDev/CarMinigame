using UnityEngine;
using TMPro;
public class MovementScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text velocityText;
    [SerializeField] private CarInput carInput;

    [Header("Movement Settings")]
    [SerializeField] private float turnSpeed = 1.2f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float deceleration = 0.1f;
    [SerializeField] private float maxSpeedKMH = 250f; // Display in km/h in the editor
    private float maxSpeed; // Internal storage in m/s

    [Header("Collision Settings")]
    [SerializeField] private float obstacleDeceleration = 500f;
    [SerializeField] private float lateralDampening = 0.5f;

    private Rigidbody rb;
    private bool isOnGround = false;

    private void Start()
    {
        maxSpeed = maxSpeedKMH / 3.6f; // Convert km/h to m/s
        InitializeRigidbody();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        DisplaySpeed();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
            ApplyObstacleDeceleration();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
            isOnGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
            isOnGround = false;
    }

    private void InitializeRigidbody()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    private void HandleMovement()
    {
        Vector2 movementInput = carInput.GetMovementInput();
        
        // Allow rotation only when the car is grounded
        if (isOnGround && Mathf.Abs(movementInput.x) > float.Epsilon) 
            RotateCar(movementInput.x);
        
        if(isOnGround)
        {
            PreventDrift();
            ApplyDrivingForce(movementInput.y);
            CapSpeed(movementInput.y);
        }
    }

    private void RotateCar(float horizontalInput)
    {
        transform.Rotate(0, horizontalInput * turnSpeed, 0);
    }

    private void PreventDrift()
    {
        Vector3 lateralVelocity = Vector3.Dot(rb.velocity, transform.right) * transform.right;
        rb.velocity -= lateralVelocity * lateralDampening;
    }

    private void ApplyDrivingForce(float verticalInput)
    {
        if (verticalInput == 0)
            rb.AddForce(-rb.velocity.normalized * deceleration, ForceMode.Acceleration);
        else
            rb.AddForce(transform.forward * verticalInput * acceleration, ForceMode.Acceleration);
    }

    private void CapSpeed(float verticalInput)
    {
        if (rb.velocity.magnitude > maxSpeed && verticalInput > 0)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    private void DisplaySpeed()
    {
        float adjustedSpeed = rb.velocity.magnitude * 3.6f; 
        velocityText.text = adjustedSpeed >= (maxSpeed * 3.6f) - 5f ? "MAX" : $"{adjustedSpeed:0.00} km/h";
    }

    private void ApplyObstacleDeceleration()
    {
        rb.AddForce(-rb.velocity.normalized * obstacleDeceleration, ForceMode.Acceleration);
    }
}
