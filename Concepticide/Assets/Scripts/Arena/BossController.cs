using UnityEngine;

public class BossController : MonoBehaviour
{
    private Animator _animator;

    private void Start() {
        _animator = GetComponent<Animator>();
    }
    
    private void Update() {
        // GetHit animation 
        if (Input.GetKeyDown(KeyCode.J)) {
            _animator.Play("GetHit");
        }

        // Punch animation 
        if (Input.GetKeyDown(KeyCode.L)) {
            _animator.Play("Attack");
        }

        // Punch animation 
        if (Input.GetKeyDown(KeyCode.M)) {
            _animator.SetBool(GameUtils.AnimVariables.Dead, true);
            Invoke(nameof(Destroy), 4);
        }
    }

    private void Destroy() {
        Destroy(gameObject);
    }
}