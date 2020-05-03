using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public string sceneName;

     public void LoadGame()
        {
          SceneManager.LoadScene(sceneName);
        }

    public void stopTheGame()
    {
        Application.Quit();
    }
}
