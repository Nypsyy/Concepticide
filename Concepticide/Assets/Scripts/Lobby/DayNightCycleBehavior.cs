using UnityEngine;
using static GameUtils;

public class DayNightCycleBehavior : MonoBehaviour
{
    public Material sun;
    public Material moon;

    public GameObject moonObj;

    public void NightTime() {
        RenderSettings.skybox = moon;
        moonObj.SetActive(true);
        moonObj.GetComponent<Animator>().SetTrigger(AnimVariables.StartNight);
    }

    public void DayTime() {
        RenderSettings.skybox = sun;
        moonObj.SetActive(false);
    }
}