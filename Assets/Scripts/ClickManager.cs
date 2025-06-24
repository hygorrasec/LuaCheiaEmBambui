using UnityEngine;
using UnityEngine.UIElements;

public class ClickManager : MonoBehaviour
{
    public void ClickItem(ItemData item) {
        TryGettingItem(item);
    }

    private void TryGettingItem(ItemData item) {
        if (item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID)) {
            GameManager.collectedItems.Add(item.itemID);
            Debug.Log("Item coletado!");
        }
    }
}
