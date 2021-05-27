using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        //GetHit animation 
        if(Input.GetKeyDown(KeyCode.J))
        {
            animator.Play("GetHit");
        }

        //Punch animation 
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.Play("Attack");
        }

        //Punch animation 
        if (Input.GetKeyDown(KeyCode.M))
        {
            animator.SetBool("Dead", true);
            Invoke("Destroy", 4);
        }

    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
