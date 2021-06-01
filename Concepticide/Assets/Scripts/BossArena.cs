using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossArena : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public Boss bossTrading;
    public Boss bossNature;
    public Boss bossMagic;

    public CombatManager combatManager;

    public GameObject tpPlayer;
    public GameObject tpBoss;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Start Combat
    void StartCombat(Concept.Id bossId) {
        Boss boss = null;
        switch (bossId) {
            case Concept.Id.Trading: boss = bossTrading; break;
            case Concept.Id.Nature: boss = bossNature; break;
            case Concept.Id.Magic: boss = bossMagic; break;
        }
        boss.transform.position = tpBoss.transform.position;
        boss.transform.rotation = tpBoss.transform.rotation;
        playerCombat.transform.position = tpPlayer.transform.position;
        playerCombat.transform.rotation = tpPlayer.transform.rotation;
        
        combatManager.boss = boss;
        combatManager.playerCombat = playerCombat;

        combatManager.gameObject.SetActive(true);
    }
}
