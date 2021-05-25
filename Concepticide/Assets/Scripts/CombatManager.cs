using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public Boss[] bosses;

    public Text infoText;

    public MenuPanel menuPanel;


    private Boss _activeBoss = null;



    // Start is called before the first frame update
    void Start()
    {
        menuPanel.PushInfo("Le combat vient de commencer !");
        Invoke("_StartCombatDebug", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void _StartCombatDebug() {
        StartCombat("Commerce");
    }

    public void StartCombat(string name) {
        if (_activeBoss != null) Debug.Log("combat already launched");
        foreach (var boss in bosses) {
            if (boss.bossName == name) {
                _activeBoss = boss;
                break;
            }
        }
        if (_activeBoss == null) Debug.Log("Unknown boss name");

        playerCombat.StartCombat(this);

        playerCombat.StartTurn();
        // Faire des trucs avec _activeBoss
    }

    public void SetInfoText(string text) {
        infoText.text = text;
    }

    public void EndTurn() {
        // Gestion de la chornologie
    }
}
