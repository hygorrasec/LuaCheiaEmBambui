using UnityEngine;
using UnityEngine.UI;

public class NPCDialogueManager : MonoBehaviour {
    public static NPCDialogueManager instance;

    [Header("UI e Áudio")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Image notificationImage;
    [SerializeField] private Image notificationBg;
    [SerializeField] private Button closeButton;

    [Header("Conteúdo do Diálogo")]
    [SerializeField] private Sprite[] dialogueImages;
    [SerializeField] private AudioClip[] dialogueAudios;

    [HideInInspector] public bool dialogueCompleted = false;

    private int currentIndex = 0;
    private bool isDialogueRunning = false;
    private AudioSource audioSource;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;

        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;

        if (panel != null)
            panel.SetActive(false);

        if (closeButton != null) {
            closeButton.onClick.RemoveAllListeners(); // garante que só tem UM listener
            closeButton.onClick.AddListener(NextDialogue);
        }
    }

    public void StartDialogueSequence() {
        if (isDialogueRunning) {
            Debug.LogWarning("⛔ Diálogo já está rodando.");
            return;
        }

        if (dialogueImages.Length == 0 || dialogueAudios.Length == 0 || panel == null || notificationImage == null || notificationBg == null) {
            Debug.LogWarning("⚠️ StartDialogueSequence abortado: algo está inválido.");
            return;
        }

        isDialogueRunning = true;
        currentIndex = 0;
        ShowCurrentDialogue();
    }

    private void ShowCurrentDialogue() {
        if (currentIndex >= dialogueImages.Length) {
            EndDialogue();
            return;
        }

        Sprite currentSprite = dialogueImages[currentIndex];
        AudioClip currentClip = dialogueAudios[currentIndex];

        Debug.Log($"📸 Mostrando imagem {currentIndex} | Áudio: {(currentClip != null ? currentClip.name : "nulo")}");

        panel.SetActive(true);
        notificationImage.sprite = currentSprite;
        notificationBg.sprite = currentSprite;

        bool isLast = currentIndex >= dialogueImages.Length - 1;
        closeButton.gameObject.SetActive(!isLast);

        if (audioSource.isPlaying)
            audioSource.Stop();

        if (currentClip != null) {
            audioSource.clip = currentClip;
            audioSource.Play();

            if (isLast)
                Invoke(nameof(EndDialogue), currentClip.length); // ⏱ Fecha automaticamente ao final
        }
        else if (isLast) {
            Invoke(nameof(EndDialogue), 0.5f); // pequena espera caso não tenha áudio
        }
    }

    public void NextDialogue() {
        if (!isDialogueRunning) return;

        Debug.Log($"🟢 NextDialogue chamado (currentIndex = {currentIndex}) | chamada única");

        if (audioSource.isPlaying)
            audioSource.Stop();

        currentIndex++;
        ShowCurrentDialogue();
    }

    private void EndDialogue() {
        isDialogueRunning = false;
        dialogueCompleted = true;

        Debug.Log("✅ Diálogo finalizado.");
        panel.SetActive(false);

        if (closeButton != null)
            closeButton.gameObject.SetActive(false);

        if (audioSource.isPlaying)
            audioSource.Stop();

        GameObject door = GameObject.Find("DoorClickableImage");
        if (door != null && door.TryGetComponent(out Collider2D col))
            col.enabled = true;
    }

    public void CloseNotification() {
        if (panel != null)
            panel.SetActive(false);

        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
