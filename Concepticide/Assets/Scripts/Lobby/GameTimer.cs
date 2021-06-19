using System.Collections;
using UnityEngine;

public delegate void TimerUpdated(int time);

public class GameTimer
{
    public int currentTime;

    public TimerUpdated onTimerCountdown;

    public GameTimer(int time) {
        currentTime = time;
    }

    public IEnumerator Countdown() {
        while (currentTime > 0) {
            currentTime -= 1;
            
            onTimerCountdown?.Invoke(currentTime);
            yield return new WaitForSeconds(1);
        }

        yield return null;
    }
}