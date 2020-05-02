using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public string SceneManager;
    public string sceneName;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
      SceneManager.LoadScene(sceneName);
    }
}
