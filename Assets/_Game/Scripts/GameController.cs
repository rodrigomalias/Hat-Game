using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // HideInInspector esconde uma variável pública do inspetor do unity
    [HideInInspector] public int score;

    private int highScore;
    private float currentTime;
    // SerializeField exibe uma variável privada no inspector para edição de valores no unity
    [SerializeField] private float startTime;

    [HideInInspector] public bool gameStarted;

    private UiController uiController;

    private SpawnController spawnController;

    [SerializeField] private Transform player;
    private Vector2 playerPosition;

    private void Awake()
    {
        // DeleteHighScore();
    }

    // Start is called before the first frame update
    void Start()
    {
        uiController = FindObjectOfType<UiController>();
        spawnController = FindObjectOfType<SpawnController>();
        gameStarted = false;
        highScore = GetScore();
        uiController.txtTime.text = currentTime.ToString();
        playerPosition = player.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyAllBalls()
    {
        foreach (Transform child in spawnController.allBallsParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void SaveScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        else
        {

            return;
        }
    }

    public int GetScore()
    {
        int highScore = PlayerPrefs.GetInt("highScore");
        return highScore;
    }

    public void DeleteHighScore()
    {
        PlayerPrefs.DeleteKey("highScore");
    }

    public void InvokeCountdownTime()
    {
        InvokeRepeating(nameof(CountDownTime), 0f, 1f);
    }

    public void StartGame()
    {
        score = 0;
        currentTime = startTime;
        gameStarted = true;
        InvokeCountdownTime();
        player.position = playerPosition;
    }

    public void BackMainMenu()
    {
        score = 0;
        currentTime = 0f;
        gameStarted = false;
        CancelInvoke(nameof(CountDownTime));
        player.position = playerPosition;
    }

    public void CountDownTime()
    {
        uiController.txtTime.text = currentTime.ToString();

        if (currentTime > 0f && gameStarted)
        {
            currentTime -= 1f;
        }
        else
        {
            uiController.panelGameOver.SetActive(true);
            uiController.panelGame.SetActive(false);
            gameStarted = false;
            SaveScore();
            currentTime = 0f;
            CancelInvoke(nameof(CountDownTime));
            return;
        }
    }
}
