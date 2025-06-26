using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {
    public static NotificationManager instance;

    [SerializeField] private GameObject panel;
    [SerializeField] private Image notificationImage;
    [SerializeField] private Image notificationBg;
    [SerializeField] private Button closeButton;
    [SerializeField] private Sprite exitDeniedImage;

    private AudioSource currentNotificationAudio;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject); // evita duplicata
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // persiste entre cenas

        if (panel != null)
            panel.SetActive(false);

        if (notificationImage != null && !notificationImage.gameObject.activeSelf)
            notificationImage.gameObject.SetActive(true);

        if (notificationBg != null && !notificationBg.gameObject.activeSelf)
            notificationBg.gameObject.SetActive(true);
    }

    public void PlayAudioIndependent(AudioClip clip, float volume = 1f) {
        if (clip == null) return;

        AudioSource tempAudio = gameObject.AddComponent<AudioSource>();
        tempAudio.clip = clip;
        tempAudio.volume = Mathf.Clamp01(volume);
        tempAudio.Play();

        Destroy(tempAudio, clip.length + 0.1f);
    }

    public void PlayNotificationAudio(AudioClip clip) {
        if (clip == null) return;

        if (currentNotificationAudio == null)
            currentNotificationAudio = gameObject.AddComponent<AudioSource>();

        currentNotificationAudio.clip = clip;
        currentNotificationAudio.Play();
    }

    public void ShowNotification(Sprite imageToShow, bool disableCloseButton = false) {
        if (notificationImage != null)
            notificationImage.sprite = imageToShow;

        if (notificationBg != null)
            notificationBg.sprite = imageToShow;

        if (panel != null)
            panel.SetActive(true);

        if (closeButton != null) {
            closeButton.interactable = !disableCloseButton;
            closeButton.gameObject.SetActive(!disableCloseButton);
            Debug.Log($"CloseButton {(disableCloseButton ? "desativado" : "ativado")}.");
        }
        else {
            Debug.LogWarning("⚠️ closeButton não atribuído no NotificationManager.");
        }
    }

    public void CloseNotification() {
        if (panel != null)
            panel.SetActive(false);

        if (closeButton != null) {
            closeButton.interactable = true;
            closeButton.gameObject.SetActive(true);
        }

        if (currentNotificationAudio != null && currentNotificationAudio.isPlaying) {
            currentNotificationAudio.Stop();
            currentNotificationAudio.clip = null;
        }
    }

    public void ShowExitDeniedWarning() {
        if (exitDeniedImage != null) {
            ShowNotification(exitDeniedImage);
        }
        else {
            Debug.LogWarning("⚠️ exitDeniedImage não atribuída no NotificationManager.");
        }
    }
}
