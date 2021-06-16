using System.Collections;
using UnityEngine;

public class GameTimer
{
    public float currentTime;

    public GameTimer(float time) {
        currentTime = time;
    }

    public IEnumerator Countdown() {
        while (currentTime > 0) {
            currentTime -= 1;
            yield return new WaitForSeconds(1);
        }

        yield return null;
    }
}