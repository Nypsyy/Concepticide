using UnityEngine;

public class BossArena : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public Boss bossTrading;
    public Boss bossNature;
    public Boss bossMagic;

    public CombatManager combatManager;

    public GameObject tpPlayer;
    public GameObject tpBoss;
    public GameObject tpSpawn;

    public GameObject thirdPersonCamera;
    public GameObject staticCam;

    public Concept concept;
    

    // Start Combat
    public void StartCombat(Concept.Id bossId) {
        Boss boss = null;
        switch (bossId) {
            case Concept.Id.Trading: boss = bossTrading; break;
            case Concept.Id.Nature: boss = bossNature; break;
            case Concept.Id.Magic: boss = bossMagic; break;
        }
        boss.transform.position = tpBoss.transform.position;
        boss.transform.rotation = tpBoss.transform.rotation;
        boss.gameObject.SetActive(true);

        // Setup the player by disabling movements and then teleporting
        playerCombat.gameObject.GetComponent<ThirdPersonMovement>().allowMove = false;
        playerCombat.gameObject.GetComponent<CharacterController>().enabled = false;
        thirdPersonCamera.SetActive(false);
        staticCam.SetActive(true);
        playerCombat.transform.position = tpPlayer.transform.position;
        playerCombat.transform.rotation = tpPlayer.transform.rotation;
        
        combatManager.boss = boss;
        combatManager.playerCombat = playerCombat;
        combatManager.endDelegate = (hasPlayerWon) => {
            // This code is executed when the combat has stopped.
            if (hasPlayerWon && concept != null)
                concept.KillConcept(bossId);
            boss.gameObject.SetActive(false);
            playerCombat.transform.position = tpSpawn.transform.position;
            playerCombat.transform.rotation = tpSpawn.transform.rotation;
            playerCombat.gameObject.GetComponent<ThirdPersonMovement>().allowMove = true;
            playerCombat.gameObject.GetComponent<CharacterController>().enabled = true;
            thirdPersonCamera.SetActive(true);
            staticCam.SetActive(false);
        };

        combatManager.StartCombat();
    }
}
