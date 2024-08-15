using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TileScene : MonoBehaviour
{
    public Image fadeImage;
    public TextMeshProUGUI textMeshPro;
    public float flashSpeed = 1.0f;
    public float fadeInDuration = 2.0f;
    public float fadeOutDuration = 2.0f;
    private Color originalColor;
    private bool fadedIn = false;
    private AudioSource audioSource;
    public AudioClip buttonClickSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        Color color = fadeImage.color;
        color.a = 1.0f;
        fadeImage.color = color;
        originalColor = textMeshPro.color;
        StartCoroutine(FadeInScene());
    }
    void Update()
    {
        float alpha = Mathf.PingPong(Time.time * flashSpeed, 1.0f);
        textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        if (fadedIn && Input.GetKeyDown(KeyCode.Return))
        {
            audioSource.PlayOneShot(buttonClickSound);
            StartCoroutine(FadeOutScene());
        }
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
        fadedIn = true;
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
