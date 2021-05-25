using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraTest : MonoBehaviour
{

    public GameObject combatManagerPrefab;

    public PlayerCombat playerCombat;

    public Boss boss;

    // Start is called before the first frame update
    void Start()
    {
        _StartCombat();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _StartCombat() {
        CombatManager newCombatManager = Instantiate(combatManagerPrefab, transform).GetComponent<CombatManager>();
        newCombatManager.boss = boss;
        newCombatManager.playerCombat = playerCombat;
        newCombatManager.endDelegate = (hasPlayerWon) => {
            _StartCombat(); // doing combat forever in this case
        };
    }
}
