using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickManager : MonoBehaviour {
    public void ClickItem(ItemData item) {
        TryGettingItem(item);
    }

    public void ClickExit(ExitData exit) {
        TryUsingExit(exit);
    }

    private void TryGettingItem(ItemData item) {
        if (item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID)) {
            if (!GameManager.collectedItems.Contains(item.itemID)) {
                GameManager.collectedItems.Add(item.itemID);
                GameManager.RegisterItem(item.itemID, item.itemName);

                if (item.itemNotificationImage != null) {
                    NotificationManager.instance.ShowNotification(item.itemNotificationImage);
                }
            }
        }
        else {
            Debug.Log($"Você precisa de outro item antes de pegar {item.itemName}.");
        }
    }

    private void TryUsingExit(ExitData exit) {
        foreach (int requiredID in exit.requiredItemIDs) {
            if (!GameManager.collectedItems.Contains(requiredID)) {
                Debug.Log("Você ainda não completou todas as ações necessárias para sair desta área.");
                return;
            }
        }

        Debug.Log("Avançando para próxima cena...");
        SceneManager.LoadScene(exit.nextSceneName);
    }
}
