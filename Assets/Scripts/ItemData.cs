using UnityEngine;

public class ItemData : MonoBehaviour {
    public int itemID;
    public int requiredItemID = -1;
    public string itemName;
    public Sprite itemNotificationImage;

    [Header("Sequência")]
    public string sceneName;
    public int sequenceOrder = 1;

    [Header("Som ao clicar corretamente")]
    public AudioClip correctSound;
}
