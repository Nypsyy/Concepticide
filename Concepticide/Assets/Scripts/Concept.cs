using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameUtils;

public class Concept : MonoBehaviour
{
    [Header("Nature")]
    public Material[] m_Nature;

    public Material m_NatureMaterial0;
    public Material m_NatureMaterial1;
    private GameObject[] m_ToHide;

    [HideInInspector]
    public bool isNatureAlive;

    [Header("Magic")] [HideInInspector]
    public bool isMagicAlive;

    [Header("Trading")] [HideInInspector]
    public bool isTradingAlive;

    public PlayerCombat m_PlayerCombat; // needed for applying magic & trading penalties
    public TextMeshProUGUI timerText;
    public int maxTime;
    public TextMeshProUGUI gameInfoText;
    public Boss bossNature; // needed to change stats
    public Boss bossMagic; // needed to change stats
    public Boss bossTrading; // needed to change stats

    private GameTimer _gameTimer;

    public enum Id
    {
        Trading,
        Nature,
        Magic
    }

    private void Start() {
        m_ToHide ??= GameObject.FindGameObjectsWithTag("Nature");

        Nature(true);
        Magic(true);
        Trading(true);

        _gameTimer = new GameTimer(maxTime);
        _gameTimer.onTimerCountdown += UpdateTimerText;
    }

    private void UpdateTimerText(int time) {
        timerText.text = TimeSpan.FromSeconds(time).Minutes.ToString("00")
                         + ":"
                         + (time % 60).ToString("00");

        switch (time) {
            case 60:
                timerText.color = Color.red;
                break;

            case 30:
                timerText.fontStyle = FontStyles.Bold;
                break;

            case 0:
                timerText.gameObject.SetActive(false);
                EndGame(false);
                break;
        }
    }

    private void OnApplicationQuit() {
        Nature(true);
    }

    public void KillConcept(Id concept) {
        Debug.Log("Concept killed: ");
        Debug.Log(concept);

        switch (concept) {
            case Id.Nature:
                bossMagic.hp += 100;

                bossTrading.attack += 20;

                m_PlayerCombat.hp += 100;
                m_PlayerCombat.defense += 10;
                
                Nature(false);
                break;

            case Id.Trading:
                bossMagic.hp += 50;
                bossMagic.attack += 10;

                bossNature.hp += 100;
                bossNature.defense += 10;
                

                m_PlayerCombat.attack += 20;

                m_PlayerCombat.hasItems = false;
                Trading(false);
                break;

            case Id.Magic:
                bossNature.hp += 50;
                bossNature.defense += 10;
                bossNature.attack += -5;

                bossTrading.hp += 50;
                bossTrading.defense += -5;
                bossTrading.attack += 10;

                m_PlayerCombat.hp += 50;
                m_PlayerCombat.defense += 5;
                m_PlayerCombat.attack += 10;

                m_PlayerCombat.hasMagic = false;
                Magic(false);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(concept), concept, null);
        }
    }

    private void Nature(bool mode) {
        if (mode) {
            m_Nature[0].SetColor(MaterialVariables.NatureColor, new Color(1f, 1f, 1f));
            m_Nature[1].SetColor(MaterialVariables.NatureColor, new Color(1f, 1f, 1f));
            m_Nature[2].SetColor(MaterialVariables.NatureColor, new Color(1f, 1f, 1f));

            foreach (var toHide in m_ToHide) {
                toHide.SetActive(true);
            }

            isNatureAlive = true;
        }

        else {
            m_Nature[0].SetColor(MaterialVariables.NatureColor, new Color(0.8f, 0.3f, 0.3f));
            m_Nature[1].SetColor(MaterialVariables.NatureColor, new Color(0.8f, 0.35f, 0.35f));
            m_Nature[2].SetColor(MaterialVariables.NatureColor, new Color(0.8f, 0.35f, 0.35f));


            // Coroutine du timer
            timerText.gameObject.SetActive(true);
            StartCoroutine(_gameTimer.Countdown());

            foreach (var toHide in m_ToHide) {
                toHide.SetActive(false);
            }

            isNatureAlive = false;

            if (IsVictory()) {
                EndGame(true);
            }
        }
    }

    private void Magic(bool mode) {
        if (mode) {
            isMagicAlive = true;
        }

        else {
            isMagicAlive = false;

            if (IsVictory()) {
                EndGame(true);
            }
        }
    }

    private void Trading(bool mode) {
        if (mode) {
            isTradingAlive = true;
        }

        else {
            isTradingAlive = false;

            if (IsVictory()) {
                EndGame(true);
            }
        }
    }

    private bool IsVictory() {
        if (isNatureAlive && isMagicAlive && isTradingAlive)
            return false;
        //if (!(!isNatureAlive & !isMagicAlive & !isTradingAlive))

        SetVictory();
        return true;
    }

    private void EndGame(bool victorious) {
        gameInfoText.text = victorious ? "YOU WON !" : "GAME OVER";
        gameInfoText.gameObject.SetActive(true);
        Invoke(nameof(Restart), 3.0f);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SetVictory() {
        StopCoroutine(_gameTimer.Countdown());
        _gameTimer.currentTime = -1;
    }
}