using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTrigger : MonoBehaviour
{
    private GameController gameController;
    private UiController uiController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        uiController = FindObjectOfType<UiController>();
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Destroyer"))
        {
            Destroy(this.gameObject);
        }

        if (target.gameObject.CompareTag("Point"))
        {
            gameController.score++;
            uiController.txtScore.text = gameController.score.ToString();
            Destroy(this.gameObject);
        }
    }
}
