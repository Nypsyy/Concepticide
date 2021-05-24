using UnityEngine;

public class Potion : MonoBehaviour
{
    public Item item;
    
    [SerializeField]
    private float amount;

    private SpriteRenderer _spriteRenderer;

    public void Awake() {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start() {
        _spriteRenderer.sprite = item.sprite;
    }
}