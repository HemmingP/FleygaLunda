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
    public void Menu()
    {
        SceneManager.LoadScene("StartScene");
        Debug.Log("LoadGame.");
    }
    public void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
        Debug.Log("Leaderboard.");
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
