using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HerbalismManager : MonoBehaviour
{
    public GameplayHerb gameplay;
    public Image fadeImage;
    public GameObject TypeList;
    public GameObject Begin;
    public float fadeInDuration = 2.0f;
    public float fadeOutDuration = 2.0f;
    private AudioSource audioSource;
    public AudioClip buttonClickSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        Color color = fadeImage.color;
        color.a = 1.0f;
        StartCoroutine(FadeInScene());
    }
    public void SinglePlayer()
    {
        audioSource.PlayOneShot(buttonClickSound);
        gameplay.SinglePlayer = true;
        TypeList.SetActive(false);
        Begin.SetActive(true);
    }
    public void MultiPlayer()
    {
        audioSource.PlayOneShot(buttonClickSound);
        gameplay.SinglePlayer = false;
        TypeList.SetActive(false);
        Begin.SetActive(true);
    }
    public void ExitGame()
    {
        audioSource.PlayOneShot(buttonClickSound);
        StartCoroutine(FadeOutScene());
    }
    IEnumerator FadeInScene()
    {
        float elapsedTime = 0.0f;
        Color color = fadeImage.color;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeInDuration);
            fadeImage.color = color;
            yield return null;
        }
        color.a = 0.0f;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(false);
    }
    IEnumerator FadeOutScene()
    {
        fadeImage.gameObject.SetActive(true);
        float elapsedTime = 0.0f;
        Color color = fadeImage.color;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeOutDuration);
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1.0f;
        fadeImage.color = color;
        SceneManager.LoadScene(1);
    }
}
