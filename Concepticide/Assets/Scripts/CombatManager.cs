using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public Boss[] bosses;

    public Text infoText;


    private Boss _activeBoss = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCombat(string name) {
        if (_activeBoss != null) Debug.Log("combat already launching");
        foreach (var boss in bosses) {
            if (boss.bossName == name) {
                _activeBoss = boss;
                break;
            }
        }
        if (_activeBoss == null) Debug.Log("Unknown boss name");

        // Faire des trucs avec _activeBoss
    }

    public void SetInfoText(string text) {
        infoText.text = text;
    }

    public void EndTurn() {
        // Gestion de la chornologie
    }
}
