using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableArea : MonoBehaviour, IPointerClickHandler {
    public NPCDialogueManager dialogueManager;

    public void OnPointerClick(PointerEventData eventData) {
        if (NPCDialogueManager.instance != null && !NPCDialogueManager.instance.dialogueCompleted) {
            NPCDialogueManager.instance.StartDialogueSequence();
        }
    }
}
