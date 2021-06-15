using Cinemachine;
using UnityEngine;
using static Utils;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform cam;
    public CinemachineFreeLook cinemachineCam;

    public bool allowMove = true;
    
    [SerializeField]
    private float speed = 6f;
    [SerializeField]
    private float turnSmoothTime = 0.1f;
    [SerializeField]
    private float turnSmoothVelocity;

    private Rigidbody _rb;
    private bool _justTeleported;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Move() {
        Vector3 moveDirection = allowMove ? new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) : Vector3.zero;
        
        if (moveDirection.magnitude >= 0.1f) {
            var targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDir.y = _rb.velocity.y;
            controller.Move(moveDir.normalized * (speed * Time.deltaTime));

        }

        if (Input.GetMouseButton(1))
        {
            cinemachineCam.m_XAxis.m_InputAxisName = "Mouse X";
            cinemachineCam.m_YAxis.m_InputAxisName = "Mouse Y";
        }
        else
        {
            cinemachineCam.m_XAxis.m_InputAxisName = "";
            cinemachineCam.m_YAxis.m_InputAxisName = "";
        }
        animator.SetFloat(AnimVariables.running, moveDirection.magnitude);
    }

    private void Update() {
        Move();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Crop>())
        {
            if (other.gameObject.GetComponent<Crop>().readyToHarvest() && Input.GetKeyDown("e"))
            {
                other.gameObject.GetComponent<Crop>().Harvest();
            }
        }
    }
}