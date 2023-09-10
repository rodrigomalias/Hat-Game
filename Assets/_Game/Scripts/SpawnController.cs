using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float topDistance, lateralMargin;

    public Transform allBallsParent;

    private Vector2 screenWidth;

    private GameController gameController;
    // Start is called before the first frame update

    // Ocorre antes do método Start
    private void Awake()
    {
        Initialize();
    }
    void Start()
    {
        InvokeRepeating(nameof(SpawnInvoke), 2.0f, Random.Range(2.0f, 3.0f));
    }

    private void Initialize()
    {
        screenWidth = Camera.main.ScreenToWorldPoint(new Vector2(Screen.safeArea.width, Screen.safeArea.height));
        Vector2 heightPosition = new(transform.position.x, Camera.main.orthographicSize + topDistance);
        transform.position = heightPosition;
        gameController = FindObjectOfType<GameController>();
    }

    private void SpawnInvoke()
    {
        // Chamar um método do tipo IEnumerator necessário usar o método StartCoroutine
        StartCoroutine(Spawn());
    }

    // tipo IEnumerator é considerado uma async function e o que estiver depois do yield acontecerá de forma automática
    private IEnumerator Spawn()
    {
        if (gameController.gameStarted)
        {
            yield return new WaitForSeconds(0f);
            transform.position = new Vector2(Random.Range(-screenWidth.x + lateralMargin, screenWidth.x - lateralMargin), transform.position.y);
            GameObject tempBallPrefab = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            tempBallPrefab.transform.parent = allBallsParent;
        }
        else
        {
            yield return null;
        }
    }
}
