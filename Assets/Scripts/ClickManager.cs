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
        if (item.itemID != GameManager.currentExpectedItemID) {
            GameManager.wrongClicks++;
            Debug.Log($"Clique fora de ordem! ({GameManager.wrongClicks} erros | +{GameManager.wrongClicks * 5} min)");
            return;
        }

        if (!GameManager.collectedItems.Contains(item.itemID)) {
            GameManager.collectedItems.Add(item.itemID);
            GameManager.RegisterItem(item.itemID, item.itemName, item);

            if (item.itemNotificationImage != null) {
                NotificationManager.instance.ShowNotification(item.itemNotificationImage);
                Debug.Log($"Item correto: {item.itemName}.");
            }

            GameManager.currentExpectedItemID++; // Avança para o próximo item esperado
        }
    }

    private void TryUsingExit(ExitData exit) {
        foreach (int requiredID in exit.requiredItemIDs) {
            if (!GameManager.collectedItems.Contains(requiredID)) {
                GameManager.wrongClicks++; // Penaliza como clique errado
                Debug.Log($"Você tentou sair antes da hora! ({GameManager.wrongClicks} erros | +{GameManager.wrongClicks * 5} min)");
                NotificationManager.instance.ShowExitDeniedWarning();
                return;
            }
        }

        Debug.Log("Avançando para a próxima cena...");
        SceneManager.LoadScene(exit.nextSceneName);
    }
}
