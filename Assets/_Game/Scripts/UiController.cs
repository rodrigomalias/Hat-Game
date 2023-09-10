using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiController : MonoBehaviour
{
    private GameController gameController;

    public GameObject panelMainMenu, panelGame, panelPause, panelGameOver;

    public TMP_Text txtHighScore, txtTime, txtScore;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        HighScoreText();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonExit()
    {
        //Forma genérica
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        //Forma Android
        // AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        // activity.Call<bool>("moveTaskToBack", true);
    }

    public void ButtonStartGame()
    {
        panelMainMenu.SetActive(false);
        panelGame.SetActive(true);
        gameController.StartGame();
    }

    public void ButtonPause()
    {
        panelGame.SetActive(false);
        panelPause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ButtonResume()
    {
        panelGame.SetActive(true);
        panelPause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ButtonRestart()
    {
        panelGame.SetActive(true);
        panelPause.SetActive(false);
        panelGameOver.SetActive(false);
        gameController.StartGame();
        gameController.DestroyAllBalls();
        txtScore.text = gameController.score.ToString();
    }

    public void ButtonBackMainMenu()
    {
        panelMainMenu.SetActive(true);
        panelPause.SetActive(false);
        panelGameOver.SetActive(false);
        gameController.BackMainMenu();
        gameController.DestroyAllBalls();
        HighScoreText();
        txtScore.text = gameController.score.ToString();
        Time.timeScale = 1f;
    }

    public void HighScoreText()
    {
        txtHighScore.text = "HighScore: " + gameController.GetScore().ToString();
    }
}
