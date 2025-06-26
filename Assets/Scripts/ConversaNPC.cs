using UnityEngine;
using UnityEngine.UI;

public class ConversaNPC : MonoBehaviour {
    public GameObject painelConversa;
    public Image imagemConversa;
    public GameObject avancarImagem;

    public Sprite[] imagensConversa;
    public AudioClip[] audiosConversa;

    private int indexAtual = 0;
    private AudioSource audioSource;

    public GameObject portaParaInoa;

    private void Start() {
        audioSource = gameObject.AddComponent<AudioSource>();
        painelConversa.SetActive(false);
        portaParaInoa.SetActive(false);
        avancarImagem.SetActive(false);
    }

    public void IniciarConversa() {
        painelConversa.SetActive(true);
        indexAtual = 0;
        MostrarFalaAtual();
    }

    private void MostrarFalaAtual() {
        imagemConversa.sprite = imagensConversa[indexAtual];
        audioSource.clip = audiosConversa[indexAtual];
        audioSource.Play();

        avancarImagem.SetActive(false);
        if (indexAtual < imagensConversa.Length - 1) {
            StartCoroutine(ExibirAvancarDepoisDoAudio());
        }
    }

    private System.Collections.IEnumerator ExibirAvancarDepoisDoAudio() {
        yield return new WaitForSeconds(audioSource.clip.length);
        avancarImagem.SetActive(true);
    }

    public void ProximaFala() {
        indexAtual++;
        if (indexAtual < imagensConversa.Length) {
            MostrarFalaAtual();
        }
        else {
            painelConversa.SetActive(false);
            portaParaInoa.SetActive(true);
        }
    }
}
