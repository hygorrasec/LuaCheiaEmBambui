using UnityEngine;

public class ImagemClicavel : MonoBehaviour {
    public ConversaNPC conversaNPC;

    private void OnMouseDown() {
        if (gameObject.activeInHierarchy) {
            conversaNPC.ProximaFala();
        }
    }
}
