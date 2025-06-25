using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {
    public static NotificationManager instance;

    public GameObject panel;
    public Image notificationImage;

    [Header("Imagens de Aviso Personalizadas")]
    public Sprite exitDeniedImage; // Nova imagem de aviso de sa�da negada

    private void Awake() {
        instance = this;

        panel.SetActive(false);

        if (notificationImage != null && !notificationImage.gameObject.activeSelf) {
            notificationImage.gameObject.SetActive(true);
        }
    }

    public void ShowNotification(Sprite imageToShow) {
        notificationImage.sprite = imageToShow;
        panel.SetActive(true);
    }

    public void CloseNotification() {
        panel.SetActive(false);
    }

    public void ShowExitDeniedWarning() {
        if (exitDeniedImage != null) {
            ShowNotification(exitDeniedImage);
        }
        else {
            Debug.LogWarning("exitDeniedImage não atribuída no NotificationManager.");
        }
    }
}
