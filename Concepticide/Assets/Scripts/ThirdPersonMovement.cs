using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform cam;
    public float speed = 6f;

    private bool justTeleported;

    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    
    // Update is called once per    
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            /*animator.SetFloat("Direction x", moveDir.x);
            animator.SetFloat("Direction z", moveDir.z);*/

            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);

            /*animator.SetFloat("Direction x", 0);
            animator.SetFloat("Direction z", 0);*/
        }

        
    }
}
