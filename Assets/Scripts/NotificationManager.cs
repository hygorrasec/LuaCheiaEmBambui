using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {
    public static NotificationManager instance;

    public GameObject panel;
    public Image notificationImage;

    private void Awake() {
        instance = this;

        // Deixa o painel oculto no começo
        panel.SetActive(false);

        // Ativa a imagem de aviso (caso esteja desativada no editor)
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
}
