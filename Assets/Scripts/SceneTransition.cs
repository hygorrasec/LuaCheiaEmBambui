using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour {
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    private void Start() {
        StartCoroutine(FadeIn());
    }

    public void TransitionToScene(string sceneName) {
        StartCoroutine(FadeAndSwitchScenes(sceneName));
    }

    private IEnumerator Transition(string sceneName) {
        yield return FadeOut();

        // 🧠 Resetar dados antes da nova cena carregar
        GameManager.ResetSceneProgress();

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeAndSwitchScenes(string sceneName) {
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeIn() {
        fadeCanvasGroup.blocksRaycasts = true; // Bloqueia interação durante o fade
        float t = fadeDuration;
        while (t > 0f) {
            t -= Time.deltaTime;
            fadeCanvasGroup.alpha = t / fadeDuration;
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.blocksRaycasts = false; // Libera interação após o fade
    }

    private IEnumerator FadeOut() {
        fadeCanvasGroup.blocksRaycasts = true; // Bloqueia interação durante fade-out
        float t = 0f;
        while (t < fadeDuration) {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = t / fadeDuration;
            yield return null;
        }
        fadeCanvasGroup.alpha = 1f;
    }
}
