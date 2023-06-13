using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Rigidbody[] targets; // Array of Rigidbody targets

    public float maxSpeed = 0.0f; // The maximum speed of the targets ** IN KM/H **

    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    [Header("UI")]
    public Text speedLabel; // The label that displays the speed;
    public RectTransform arrow; // The arrow in the speedometer

    private float speed = 0.0f;

    private void Update()
    {
        // Calculate the maximum speed among all targets
        float maxTargetSpeed = 0.0f;
        foreach (Rigidbody target in targets)
        {
            float targetSpeed = target.velocity.magnitude * 3.6f;
            if (targetSpeed > maxTargetSpeed)
                maxTargetSpeed = targetSpeed;
        }

        // Use the maximum speed as the current speed
        speed = maxTargetSpeed;

        if (speedLabel != null)
            speedLabel.text = ((int)speed) + " km/h";
        if (arrow != null)
            arrow.localEulerAngles =
                new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed / maxSpeed));
    }
}
