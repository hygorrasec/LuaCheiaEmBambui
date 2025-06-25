using UnityEngine;

public class ItemData : MonoBehaviour {
    public int itemID;
    public int requiredItemID = -1;
    public string itemName;
    public Sprite itemNotificationImage;

    [Header("Sequência")]
    public string sceneName;      // Nome da cena onde está o item
    public int sequenceOrder = 1; // Ordem da interação dentro da cena
}
