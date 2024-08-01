using System.Collections;
using System.Collections.Generic;
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
        Color color = fadeImage.color;
        color.a = 1.0f;
        StartCoroutine(FadeInScene());
        audioSource = gameObject.AddComponent<AudioSource>();
        buttonClickSound = Resources.Load<AudioClip>("Audio/buttonClick");
        audioSource.playOnAwake = false; 

        if (buttonClickSound == null)
        {
            Debug.LogError("Failed to load button click sound. Please ensure the file is located at Assets/Resources/Audio/buttonClick");
        }
    }
    public void GameModeEasy()
    {
        PlayButtonClickSound();
        Instantiate(EasyMode);
        LevelList.SetActive(false);
    }

    public void GameModeMedium()
    {
        PlayButtonClickSound();
        Instantiate(MediumMode);
        LevelList.SetActive(false);
    }
    public void GameModeHard()
    {
        PlayButtonClickSound();
        Instantiate(HardMode);
        LevelList.SetActive(false);
    }
    public void ExitGame()
    {
        PlayButtonClickSound();
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
    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
        else
        {
            Debug.LogWarning("Button click sound is null.");
        }
    }
}