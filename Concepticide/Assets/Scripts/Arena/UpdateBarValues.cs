using UnityEngine;
using UnityEngine.UI;

public class UpdateBarValues : MonoBehaviour
{
    public Text BarText;

    public void UpdateText(float value) {
        BarText.text = value.ToString();
    }
}