using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExit : MonoBehaviour {
    public NPCDialogueManager dialogueManager; // Referência ao NPCDialogueManager
    public string sceneToLoad = "Inoã";

    private void OnMouseDown() {
        if (dialogueManager != null && dialogueManager.dialogueCompleted) {
            Debug.Log("✅ Jogador conversou com o NPC. Indo para a próxima cena...");
            SceneManager.LoadScene(sceneToLoad);
        }
        else {
            Debug.Log("❌ Você precisa conversar com o NPC antes de sair.");
            NotificationManager.instance?.ShowExitDeniedWarning();
        }
    }
}
