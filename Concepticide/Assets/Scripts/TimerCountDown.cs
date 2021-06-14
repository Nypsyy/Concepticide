using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountDown : MonoBehaviour
{
    float currentTime = 0f;
    float countDownStartValue = 300f;
    [SerializeField] Text timeText ;


    public void StartCountDown()
    {
        currentTime = countDownStartValue;
        timeText.text = currentTime.ToString("0");
        Invoke("CountDown", 1.0f);
    }

    public void CountDown()
    {
        currentTime -= 1;
        

        if(currentTime <= 60)
        {
            timeText.color = Color.red;
            if (currentTime <= 30)
            {
                timeText.fontSize = 50;
            }
        }

        if (currentTime <= 0 & currentTime > -3)
        {
            currentTime = 0;
            timeText.fontSize = 30;
            //timeText.rectTransform.sizeDelta = new Vector2(600,80);
            timeText.text = "Game Over !";
            //gameOver
            FindObjectOfType<Concept>().EndGame();
        }

        if (currentTime > 0)
        {
            //Debug.Log(currentTime);
            timeText.text = currentTime.ToString("0");
            Invoke("CountDown", 1.0f);
        }
    }

    public void AnnounceVictory()
    {
        currentTime = -4;
        timeText.fontSize = 30;
        timeText.color = Color.red;
        timeText.text = "You won !";
    }
}
