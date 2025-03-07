using UnityEngine;
using System.Collections;

public class L103Manager : MonoBehaviour
{
    public static L103Manager Instance;

    [SerializeField] private GameObject winPopupPrefab;
    [SerializeField] private GameObject losePopupPrefab;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private Canvas canvas;

    private int currentScore = 0;
    private int winScore = 100;
    private bool isGameOver = false;
    private float timeLimit = 45f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();

        StartCoroutine(StartCountdown());
    }

    public void AddScore(int score)
    {
        if (isGameOver) return;

        currentScore += score;
        Debug.Log("Current Score: " + currentScore);

        if (currentScore >= winScore)
        {
            WinGame();
        }
    }

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(timeLimit);
        if (!isGameOver)
        {
            LoseGame();
        }
    }

    private void WinGame()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (gameplayPanel != null) gameplayPanel.SetActive(false);
        if (winPopupPrefab != null && canvas != null)
        {
            winPopupPrefab.SetActive(true);
        }
    }

    private void LoseGame()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (gameplayPanel != null) gameplayPanel.SetActive(false);
        if (losePopupPrefab != null && canvas != null)
        {
            losePopupPrefab.SetActive(true);
        }
    }
}