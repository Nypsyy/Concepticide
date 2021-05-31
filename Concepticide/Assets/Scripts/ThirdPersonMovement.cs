using System;
using UnityEngine;
using static Utils;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    private bool justTeleported;

    private void Move() {
        var moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (moveDirection.magnitude >= 0.1f) {
            var targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * (speed * Time.deltaTime));
            /*animator.SetFloat("Direction x", moveDir.x);
            animator.SetFloat("Direction z", moveDir.z);*/

            animator.SetBool(AnimVariables.IsRunning, true);
        }
        else
            animator.SetBool(AnimVariables.IsRunning, false);
    }

    private void Update() => Move();
}