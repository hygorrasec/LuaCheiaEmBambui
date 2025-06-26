using UnityEngine;
using System.Collections;

public class CanvasAutoActivator : MonoBehaviour {
    private void Awake() {
        StartCoroutine(EnableCanvasAfterFrame());
    }

    private IEnumerator EnableCanvasAfterFrame() {
        yield return new WaitForEndOfFrame();

        // Busca todos os Canvas, inclusive inativos
        Canvas[] allCanvases = Resources.FindObjectsOfTypeAll<Canvas>();
        foreach (Canvas canvas in allCanvases) {
            if (canvas.name == "CanvasTransition") {
                GameObject canvasGO = canvas.gameObject;
                if (!canvasGO.activeSelf) {
                    canvasGO.SetActive(true);
                    Debug.Log("✅ CanvasTransition ativado com sucesso.");
                }
                else {
                    Debug.Log("CanvasTransition já estava ativo.");
                }
                yield break; // terminou
            }
        }

        Debug.LogError("❌ CanvasTransition não encontrado, mesmo entre objetos inativos.");
    }
}
