using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {
    public static MusicManager instance;

    [System.Serializable]
    public class SceneMusic {
        public string sceneName;
        public AudioClip musicClip;
        [Range(0f, 1f)] public float volume = 0.5f; // Volume individual
    }

    [Header("Configura��o")]
    public SceneMusic[] musics;
    public float defaultVolume = 0.5f; // Fallback se n�o encontrar a cena

    private AudioSource audioSource;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName) {
        foreach (var entry in musics) {
            if (entry.sceneName == sceneName && entry.musicClip != null) {
                if (audioSource.clip != entry.musicClip) {
                    audioSource.clip = entry.musicClip;
                    audioSource.volume = entry.volume;
                    audioSource.Play();
                }
                return;
            }
        }

        // Se nenhuma m�sica foi definida para a cena, para o �udio
        audioSource.Stop();
    }
}
