
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    private GameObject smallCrop;
    private GameObject mediumCrop;
    private GameObject largeCrop;

    private ParticleSystem FX;

    private BoxCollider _collider;

    private bool toGrow = false;
        
    void Start()
    {
        smallCrop = gameObject.transform.GetChild (0).gameObject;
        mediumCrop = gameObject.transform.GetChild (1).gameObject;
        largeCrop = gameObject.transform.GetChild (2).gameObject;

        FX = gameObject.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>();

        _collider = gameObject.GetComponent<BoxCollider>();
        
        InvokeRepeating(nameof(WaitForGrow),5,5);
        
    }
    
    void Update()
    {
        if (toGrow)
        {
            if (smallCrop.activeSelf)
            {
                if (Random.Range(0, 100) <= 50f)
                {
                    smallCrop.SetActive(false);
                    mediumCrop.SetActive(true);
                }

                toGrow = false;
            }

            else if(mediumCrop.activeSelf)
            {
                if (Random.Range(0, 100) <= 50f)
                {
                    mediumCrop.SetActive(false);
                    largeCrop.SetActive(true);
                }

                toGrow = false;
            }
        }

        if (readyToHarvest())
            gameObject.tag = "GroundItem";

        else
            gameObject.tag = "Untagged";
    }
    
    void WaitForGrow()
    {
        toGrow = true;
    }

    public bool readyToHarvest()
    {
        return largeCrop.activeSelf ? true : false;
    }

    public void Harvest()
    {
        largeCrop.SetActive(false);
        smallCrop.SetActive(true);
        
        FX.Play();
        
        gameObject.tag = "Untagged";
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (readyToHarvest())
        {
            Harvest();
        }
    }
}
