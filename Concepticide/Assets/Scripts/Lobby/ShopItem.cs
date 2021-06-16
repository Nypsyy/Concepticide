using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Concept m_Concept;
    public string m_Item;
    public RawImage itemImage;
    public TextMeshProUGUI itemPrice;

    private bool isForbiden;

    private void Start() {
        var textureImage = Resources.Load<Texture>(m_Item);

        if (textureImage != null) {
            itemImage.texture = textureImage;
            itemImage.color = new Color(255, 255, 255, 100);
            itemPrice.text = "100 G";
        }

        Debug.Log("oui");
    }

    private void Update() {
        if (m_Concept.isMagicAlive || m_Item != "manaPotion") return;

        itemImage.color = new Color(255, 255, 255, 0);
        itemPrice.text = "";
    }

    public void BuyItem() {
        Debug.Log("J'ai acheté une " + m_Item);
    }
}