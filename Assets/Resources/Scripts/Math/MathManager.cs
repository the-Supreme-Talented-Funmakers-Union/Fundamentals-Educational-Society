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
    public void GameModeEasy()
    {
        Instantiate(EasyMode);
        LevelList.SetActive(false);
    }
    public void GameModeMedium()
    {
        Instantiate(MediumMode);
        LevelList.SetActive(false);
    }
    public void GameModeHard()
    {
        Instantiate(HardMode);
        LevelList.SetActive(false);
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(1);
    }
}
