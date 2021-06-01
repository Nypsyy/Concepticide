using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concept : MonoBehaviour
{

    [Header("Nature")]
    public Material[] m_Nature;
    public Material m_NatureMaterial0;
    public Material m_NatureMaterial1;
    private GameObject[] m_ToHide;
    [HideInInspector] public bool isNatureAlive;

    [Header("Magic")] 
    [HideInInspector] public bool isMagicAlive;
    
    [Header("Trading")]
    [HideInInspector] public bool isTradingAlive;

    public PlayerCombat m_PlayerCombat; // needed for applying magic & trading penalties

    public enum Id { Trading, Nature, Magic };


    void Start()
    {
        if (m_ToHide == null)
            m_ToHide = GameObject.FindGameObjectsWithTag("Nature");
        Nature(true);
        Magic(true);
        Trading(true);
        
    }
    
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        Nature(true);
    }

    public void KillConcept(Id concept) {
        Debug.Log("Concept killed: ");
        Debug.Log(concept);
        switch(concept) {
            case Id.Nature: Nature(false); break;
            case Id.Trading: m_PlayerCombat.hasItems = false; Trading(false); break;
            case Id.Magic: m_PlayerCombat.hasMagic = false; Magic(false); break;
        }
    }

    void Nature(bool mode)
    {
        if (mode)
        {
            m_Nature[0].SetColor("_Color", new Color(1f,1f,1f));
            m_Nature[1].SetColor("_Color", new Color(1f,1f,1f));
            m_Nature[2].SetColor("_Color", new Color(1f,1f,1f));
            
            foreach (GameObject toHide in m_ToHide)
            {
                toHide.SetActive(true);
            }

            isNatureAlive = true;

        }

        else
        {
            m_Nature[0].SetColor("_Color", new Color(0.8f,0.3f,0.3f));
            m_Nature[1].SetColor("_Color", new Color(0.8f,0.35f,0.35f));
            m_Nature[2].SetColor("_Color", new Color(0.8f,0.35f,0.35f));
            
            foreach (GameObject toHide in m_ToHide)
            {
                toHide.SetActive(false);
            }

            isNatureAlive = false;
        }
    }

    void Magic(bool mode)
    {
        if (mode)
        {
            isMagicAlive = true;
        }

        else
        {
            isMagicAlive = false;
        }
    }

    void Trading(bool mode)
    {
        if (mode)
        {
            isTradingAlive = true;
        }

        else
        {
            isTradingAlive = false;
        }
    }
}
