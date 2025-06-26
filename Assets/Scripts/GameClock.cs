using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameClock : MonoBehaviour {
    public TMP_Text clockText;

    private float elapsedTime = 0f;
    private int minutesPassed = 0;
    private int baseHour = 16;

    private static GameClock instance;
    private bool invasionTriggered = false;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        minutesPassed = GameManager.savedMinutesPassed;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        GameObject textObj = GameObject.FindGameObjectWithTag("ClockText");
        if (textObj != null) {
            clockText = textObj.GetComponent<TMP_Text>();
        }
    }

    void Update() {
        if (clockText == null) return;

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f) {
            minutesPassed += 1;
            elapsedTime = 0f;
        }

        GameManager.savedMinutesPassed = minutesPassed;

        int penaltyMinutes = GameManager.wrongClicks * 1;
        int totalMinutes = minutesPassed + penaltyMinutes;

        int hour = baseHour + (totalMinutes / 60);
        int minute = totalMinutes % 60;

        clockText.text = $"{hour:00}:{minute:00}h / 22:00h";

        // 🔥 VERIFICAÇÃO DE INVASÃO DO LOBISOMEM
        if (!invasionTriggered && hour >= 22) {
            invasionTriggered = true;
            Debug.Log("⏰ Hora crítica atingida! Lobisomem está chegando...");
            SceneManager.LoadScene("Invasão do Lobisomem");
        }
    }
}
