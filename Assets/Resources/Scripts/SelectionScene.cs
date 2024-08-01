using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionScene : MonoBehaviour
{
    public Image fadeImage;
    public float fadeInDuration = 2.0f;
    public float fadeOutDuration = 2.0f;
    void Start()
    {
        Color color = fadeImage.color;
        color.a = 1.0f;
        StartCoroutine(FadeInScene());
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
    IEnumerator FadeOutScene(int s)
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
        if (s != -1)
        {
            SceneManager.LoadScene(s);
        }
        else
        {
            Application.Quit();
        }
    }
    public void ToMath()
    {
        StartCoroutine(FadeOutScene(2));
    }
    public void ToHerbalism()
    {
        StartCoroutine(FadeOutScene(3));
    }
    public void ExitGame()
    {
        StartCoroutine(FadeOutScene(-1));
    }
}
