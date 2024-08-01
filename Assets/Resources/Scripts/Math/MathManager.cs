using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MathManager : MonoBehaviour
{
    public GameObject EasyMode;
    public GameObject MediumMode;
    public GameObject HardMode;
    public GameObject LevelList;

    private AudioSource audioSource;
    public AudioClip buttonClickSound;

    void Start()
    {
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