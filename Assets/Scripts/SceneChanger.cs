using UnityEngine;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Image blackImage;
    public string sceneName;
    public float fadeDuration = 1f;
    private bool fadeInProgress = false;

    void Update()
    {
        if (fadeInProgress && blackImage != null)
        {
            blackImage.color = new Color(0, 0, 0, Mathf.Min(blackImage.color.a + Time.deltaTime / fadeDuration, 1));
        }
        if (fadeInProgress && blackImage != null && blackImage.color.a >= 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }

    public void ChangeScene()
    {
        if (blackImage == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            return;
        }

        fadeInProgress = true;
        blackImage.gameObject.SetActive(true);
    }
}
