using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public void PlayAgain()
    {
        //Application.LoadLevel("MainScene");
        SceneManager.LoadScene("MainScene");
        
    }

    public void GoToHiScores()
    {
        SceneManager.LoadScene("HiScores");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Intro");
    }

    public void GoToCheatMenu()
    {
        SceneManager.LoadScene("CheatMenu");
    }

    public void GoToCarGameMenu()
    {
        SceneManager.LoadScene("CarGameIntro");
    }

    public void CarGameMain()
    {
        SceneManager.LoadScene("TetrisCar");
    }

    public void CarGameScores()
    {
        SceneManager.LoadScene("TetrisCarScores");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
