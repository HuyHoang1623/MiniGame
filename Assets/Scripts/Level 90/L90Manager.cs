using UnityEngine;
using System.Collections;

public class L90Manager : MonoBehaviour
{
    public static L90Manager Instance; 

    [SerializeField] private GameObject winPopup;   
    [SerializeField] private GameObject losePopup;  
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private float timeLimit = 30f;

    private bool gameOver = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(LoseCountdown());
    }

    private IEnumerator LoseCountdown()
    {
        yield return new WaitForSeconds(timeLimit);
        if (!gameOver)
        {
            EndGame(false);
        }
    }

    public void EndGame(bool isWin)
    {
        if (gameOver) return;
        gameOver = true;

        if (isWin)
        {
            gamePlayPanel.SetActive(false);
            if (winPopup != null) winPopup.SetActive(true);
        }
        else
        {
            gamePlayPanel.SetActive(false);
            if (losePopup != null) losePopup.SetActive(true);
        }
    }
}