using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingMotor : MonoBehaviour
{
    private Image loadingIndicator;
    [SerializeField] private string sceneName;

    private void Start()
    {
        loadingIndicator = GameObject.Find("Bar").GetComponent<Image>();
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        AsyncOperation result = SceneManager.LoadSceneAsync(sceneName);
        result.allowSceneActivation = false;

        while (!result.isDone)
        {

            float progress = Mathf.Clamp01(result.progress / 0.9f);
            loadingIndicator.fillAmount = progress * 100;

            if (result.progress == 0.9f)
            {
                result.allowSceneActivation = true;
            }

            yield return null;

        }

    }
}
