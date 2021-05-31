using System;
using UnityEngine;
using static Utils;

public class ThirdPersonMovement : MonoBehaviour
{
    private Actions _playerActions;
    public CharacterController controller;
    public Animator animator;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    private bool justTeleported;

    private void Move() {
        var moveDirection = _playerActions.Player.Move.ReadValue<Vector2>().normalized;

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


    private void Awake() {
        _playerActions = new Actions();
    }

    private void OnEnable() {
        _playerActions.Enable();
    }

    private void OnDisable() {
        _playerActions.Disable();
    }

    private void Update() => Move();
}