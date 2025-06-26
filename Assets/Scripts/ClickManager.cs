using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ClickManager : MonoBehaviour {
    private bool isExiting = false;

    public void ClickItem(ItemData item) {
        TryGettingItem(item);
    }

    public void ClickExit(ExitData exit) {
        if (!isExiting) {
            StartCoroutine(TryUsingExitWithSound(exit));
        }
    }

    private void TryGettingItem(ItemData item) {
        if (GameManager.collectedItems.Contains(item.itemID)) {
            Debug.Log($"Item já coletado: {item.itemName}.");
            return;
        }

        if (item.sequenceOrder != GameManager.currentExpectedItemID) {
            GameManager.wrongClicks++;
            Debug.Log($"Clique fora de ordem! ({GameManager.wrongClicks} erros | +{GameManager.wrongClicks * 1} min)");
            return;
        }

        GameManager.collectedItems.Add(item.itemID);
        GameManager.RegisterItem(item.itemID, item.itemName, item);

        if (item.itemNotificationImage != null) {
            NotificationManager.instance.ShowNotification(item.itemNotificationImage);
            Debug.Log($"Item correto: {item.itemName}.");
        }

        if (item.correctSound != null) {
            NotificationManager.instance.PlayNotificationAudio(item.correctSound);
        }

        GameManager.currentExpectedItemID++;
    }

    private IEnumerator TryUsingExitWithSound(ExitData exit) {
        isExiting = true;

        foreach (int requiredID in exit.requiredItemIDs) {
            if (!GameManager.collectedItems.Contains(requiredID)) {
                GameManager.wrongClicks++;
                Debug.Log($"Você tentou sair antes da hora! ({GameManager.wrongClicks} erros | +{GameManager.wrongClicks * 1} min)");
                NotificationManager.instance.ShowExitDeniedWarning();
                isExiting = false;
                yield break;
            }
        }

        Debug.Log("Item final correto! Tocando áudio...");

        if (exit.exitNotificationImage != null) {
            NotificationManager.instance.ShowNotification(exit.exitNotificationImage, disableCloseButton: true);
        }

        // Toca soundText e exitSound ao mesmo tempo, se existirem
        float waitTime = 0f;

        if (exit.soundText != null) {
            NotificationManager.instance.PlayAudioIndependent(exit.soundText);
            waitTime = Mathf.Max(waitTime, exit.soundText.length);
        }

        if (exit.exitSound != null) {
            NotificationManager.instance.PlayAudioIndependent(exit.exitSound, 0.1f);
            waitTime = Mathf.Max(waitTime, exit.exitSound.length);
        }

        yield return new WaitForSeconds(waitTime + 0.1f);

        if (NotificationManager.instance != null) {
            NotificationManager.instance.CloseNotification();
        }

        SceneTransition st = Object.FindFirstObjectByType<SceneTransition>();
        if (st != null) {
            st.TransitionToScene(exit.nextSceneName);
        }
        else {
            Debug.LogError("SceneTransition não encontrado!");
        }
    }
}
