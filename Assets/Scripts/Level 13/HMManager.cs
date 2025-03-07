using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMManager : MonoBehaviour
{
    public static HMManager instance;
    private int _mouseCount;

    public delegate void WinEvent();
    public event WinEvent OnWin;

    public delegate void LoseEvent();
    public event LoseEvent OnLose;

    [SerializeField] private GameObject winPopupPrefab;
    [SerializeField] private GameObject losePopupPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private float timeLimit = 30f;
    private bool isGameOver = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }
    }

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }

    public void RegisterMouse()
    {
        _mouseCount++;
    }

    public void UnregisterMouse()
    {
        _mouseCount--;

        if (_mouseCount <= 0 && !isGameOver)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        if (isGameOver) return;
        isGameOver = true;

        OnWin?.Invoke();

        if (gameplayPanel != null)
        {
            gameplayPanel.SetActive(false);
        }

        if (winPopupPrefab != null && canvas != null)
        {
            winPopupPrefab.SetActive(true);
        }
    }

    private void LoseGame()
    {
        if (isGameOver) return;
        isGameOver = true;

        OnLose?.Invoke();

        if (gameplayPanel != null)
        {
            gameplayPanel.SetActive(false);
        }

        if (losePopupPrefab != null && canvas != null)
        {
            GameObject losePopup = Instantiate(losePopupPrefab, canvas.transform);
            losePopup.SetActive(true);
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
}
