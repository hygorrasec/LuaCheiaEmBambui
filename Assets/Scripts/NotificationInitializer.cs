using UnityEngine;

public class NotificationInitializer : MonoBehaviour {
    [SerializeField] private GameObject notificationManagerPrefab;

    private void Awake() {
        if (NotificationManager.instance == null) {
            GameObject obj = Instantiate(notificationManagerPrefab);
            obj.name = "NotificationManager";
        }
    }
}
