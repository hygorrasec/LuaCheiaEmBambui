using UnityEngine;
using TMPro; // Importante para TextMeshPro

public class GameClock : MonoBehaviour {
    public TMP_Text clockText; // Use TMP_Text em vez de Text

    private float elapsedTime = 0f;
    private int minutesPassed = 0;
    private int baseHour = 16;

    void Update() {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f) {
            minutesPassed += 1;
            elapsedTime = 0f;
        }

        int penaltyMinutes = GameManager.wrongClicks * 5;
        int totalMinutes = minutesPassed + penaltyMinutes;

        int hour = baseHour + (totalMinutes / 60);
        int minute = totalMinutes % 60;

        clockText.text = $"{hour:00}:{minute:00}h";
    }
}
