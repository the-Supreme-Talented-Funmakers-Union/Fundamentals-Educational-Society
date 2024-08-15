using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MathManager : MonoBehaviour
{
    public GameObject EasyMode;
    public GameObject MediumMode;
    public GameObject HardMode;
    public GameObject LevelList;
    public Image fadeImage;
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
    public void GameModeEasy()
    {
        audioSource.PlayOneShot(buttonClickSound);
        Instantiate(EasyMode);
        LevelList.SetActive(false);
    }

    public void GameModeMedium()
    {
        audioSource.PlayOneShot(buttonClickSound);
        Instantiate(MediumMode);
        LevelList.SetActive(false);
    }
    public void GameModeHard()
    {
        audioSource.PlayOneShot(buttonClickSound);
        Instantiate(HardMode);
        LevelList.SetActive(false);
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