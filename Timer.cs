using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer")]
    public float countDownTimer = 5f;

    [Header("Things to stop")]
    public PlayerCarController playerCarController;
    public PlayerCarController playerCarController1;
    public PlayerCarController playerCarController2;
    public OpponentCar opponentCar;
    public OpponentCar opponentCar1;
    public OpponentCar opponentCar2;
    public OpponentCar opponentCar3;
    public OpponentCar opponentCar4;

    public Text countDownText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeCount());
    }

    // Update is called once per frame
    void Update()
    {
        if (countDownTimer > 1)
        {
            playerCarController.accelerationForce = 0f;
            playerCarController1.accelerationForce = 0f;
            playerCarController2.accelerationForce = 0f;
            opponentCar.movingSpeed = 0f;
            opponentCar1.movingSpeed = 0f;
            opponentCar2.movingSpeed = 0f;
            opponentCar3.movingSpeed = 0f;
            opponentCar4.movingSpeed = 0f;
        }
        else if (countDownTimer == 0)
        {
            playerCarController.accelerationForce = 300f;
            playerCarController1.accelerationForce = 300f;
            playerCarController2.accelerationForce = 300f;
            opponentCar.movingSpeed = 12f;
            opponentCar1.movingSpeed = 13f;
            opponentCar2.movingSpeed = 14f;
            opponentCar3.movingSpeed = 9f;
            opponentCar4.movingSpeed = 8f;
        }
    }

    IEnumerator TimeCount()
    {
        while (countDownTimer > 0)
        {
            countDownText.text = countDownTimer.ToString();
            yield return new WaitForSeconds(1f);
            countDownTimer--;
        }

        countDownText.text = "60";
        yield return new WaitForSeconds(1f);
        countDownText.gameObject.SetActive(false);
    }
}
