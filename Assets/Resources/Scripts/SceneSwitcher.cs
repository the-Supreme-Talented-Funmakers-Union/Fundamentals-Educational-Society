using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchScene1()
    {
        SceneManager.LoadScene(1);
    }
    public void SwitchScene2()
    {
        SceneManager.LoadScene(2);
    }
    public void SwitchScene3()
    {
        SceneManager.LoadScene(3);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}