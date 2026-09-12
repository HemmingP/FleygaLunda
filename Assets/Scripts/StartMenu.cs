using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartNewGame()
    {
        Debug.Log("StartNewGamefound.");
    }
    public void LoadGame()
    {
        Debug.Log("LoadGame.");
    }
    public void Options()
    {
        Debug.Log("Options.");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
        Debug.Log("Credits.");
    }
    public void Exit()
    {
        Debug.Log("Exit.");
        Application.Quit();
    }
}
