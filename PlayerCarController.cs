using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [Header("Wheels collider")]
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider backLeftWheelCollider;
    public WheelCollider backRightWheelCollider;

    [Header("Wheels Transform")]
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform backLeftWheelTransform;
    public Transform backRightWheelTransform;

    [Header("Car Engine")]
    public float accelerationForce = 300f;
    public float maxSpeed = 100f;
    public float brakingForce = 5000f;
    private float presentAcceleration = 0f;

    [Header("Car Steering")]
    public float maxSteerAngle = 40f;
    public float steerSpeed = 10f;
    private float presentSteerAngle = 0f;

    [Header("Car Sounds")]
    public AudioSource audioSource;
    public AudioClip accelerationSound;
    public AudioClip slowAccelerationSound;
    public AudioClip stopSound;
    public AudioClip brakeSound; // New brake sound clip

    private bool isDrifting = false;
    private float driftAngle = 40f;

    private void Update()
    {
        MoveCar();
        CarSteering();
        ApplyBrakes();
    }

    private void MoveCar()
    {
        float verticalInput = Input.GetAxis("Vertical");
        presentAcceleration = verticalInput * accelerationForce;

        if (presentAcceleration > 0)
        {
            if (!audioSource.isPlaying || audioSource.clip != accelerationSound)
            {
                audioSource.Stop();
                audioSource.clip = accelerationSound;
                audioSource.Play();
            }
        }
        else if (presentAcceleration < 0)
        {
            if (!audioSource.isPlaying || audioSource.clip != slowAccelerationSound)
            {
                audioSource.Stop();
                audioSource.clip = slowAccelerationSound;
                audioSource.Play();
            }
        }
        else
        {
            if (!audioSource.isPlaying || audioSource.clip != stopSound)
            {
                audioSource.Stop();
                audioSource.clip = stopSound;
                audioSource.Play();
            }
        }

        frontLeftWheelCollider.motorTorque = presentAcceleration;
        frontRightWheelCollider.motorTorque = presentAcceleration;
        backLeftWheelCollider.motorTorque = presentAcceleration;
        backRightWheelCollider.motorTorque = presentAcceleration;

        if (GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
        }
    }

    private void CarSteering()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        presentSteerAngle = horizontalInput * maxSteerAngle;

        // Apply additional steer angle during drifting
        if (isDrifting)
        {
            frontLeftWheelCollider.steerAngle = Mathf.Lerp(frontLeftWheelCollider.steerAngle, presentSteerAngle + driftAngle, Time.deltaTime * steerSpeed);
            frontRightWheelCollider.steerAngle = Mathf.Lerp(frontRightWheelCollider.steerAngle, presentSteerAngle - driftAngle, Time.deltaTime * steerSpeed);
        }
        else
        {
            // Apply regular steering
            frontLeftWheelCollider.steerAngle = Mathf.Lerp(frontLeftWheelCollider.steerAngle, presentSteerAngle, Time.deltaTime * steerSpeed);
            frontRightWheelCollider.steerAngle = Mathf.Lerp(frontRightWheelCollider.steerAngle, presentSteerAngle, Time.deltaTime * steerSpeed);
        }

        SteeringWheels(frontLeftWheelCollider, frontLeftWheelTransform);
        SteeringWheels(frontRightWheelCollider, frontRightWheelTransform);
        SteeringWheels(backLeftWheelCollider, backLeftWheelTransform);
        SteeringWheels(backRightWheelCollider, backRightWheelTransform);
    }

    private void SteeringWheels(WheelCollider WC, Transform WT)
    {
        Vector3 position;
        Quaternion rotation;

        WC.GetWorldPose(out position, out rotation);

        WT.position = position;
        WT.rotation = rotation;
    }

    private void ApplyBrakes()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            frontLeftWheelCollider.brakeTorque = brakingForce;
            frontRightWheelCollider.brakeTorque = brakingForce;
            backLeftWheelCollider.brakeTorque = brakingForce;
            backRightWheelCollider.brakeTorque = brakingForce;

            if (!audioSource.isPlaying || audioSource.clip != brakeSound)
            {
                audioSource.Stop();
                audioSource.clip = brakeSound;
                audioSource.Play();
            }

            // Check for shift key press to toggle drifting
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                isDrifting = !isDrifting;
            }
        }
        else
        {
            frontLeftWheelCollider.brakeTorque = 0f;
            frontRightWheelCollider.brakeTorque = 0f;
            backLeftWheelCollider.brakeTorque = 0f;
            backRightWheelCollider.brakeTorque = 0f;

            // Stop brake sound when brakes are released
            if (audioSource.isPlaying && audioSource.clip == brakeSound)
            {
                audioSource.Stop();
            }
        }
    }
}
