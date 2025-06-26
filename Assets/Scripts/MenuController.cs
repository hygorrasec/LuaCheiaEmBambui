using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public void StartGame() {
        SceneManager.LoadScene("Bar");
    }

    public void QuitGame() {
        Application.Quit();

        // Para editor, apenas por seguran�a ao testar
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
