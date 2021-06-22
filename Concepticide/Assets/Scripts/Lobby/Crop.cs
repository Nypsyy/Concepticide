using UnityEngine;

public class Crop : MonoBehaviour
{
    public ItemObject item;

    private GameObject _smallCrop;
    private GameObject _mediumCrop;
    private GameObject _largeCrop;

    private ParticleSystem _fx;

    private bool IsHarvestable => _largeCrop.activeSelf;

    private void Start() {
        _smallCrop = gameObject.transform.GetChild(0).gameObject;
        _mediumCrop = gameObject.transform.GetChild(1).gameObject;
        _largeCrop = gameObject.transform.GetChild(2).gameObject;

        _fx = gameObject.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>();

        InvokeRepeating(nameof(TryGrow), 5, 5);
    }

    private void TryGrow() {
        if (_smallCrop.activeSelf) {
            if (Random.Range(0, 100) > 50) return;

            _smallCrop.SetActive(false);
            _mediumCrop.SetActive(true);
        }
        else if (_mediumCrop.activeSelf) {
            if (Random.Range(0, 100) > 50) return;

            _mediumCrop.SetActive(false);
            _largeCrop.SetActive(true);
        }
    }

    private void Harvest() {
        _largeCrop.SetActive(false);
        _smallCrop.SetActive(true);

        _fx.Play();
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player"))
            return;

        if (!IsHarvestable)
            return;

        if (other.GetComponent<InventoryManager>().inventory.AddItem(new Item(item), 1))
            Harvest();
    }
}