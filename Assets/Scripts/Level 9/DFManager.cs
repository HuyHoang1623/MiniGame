using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFManager : MonoBehaviour
{
    public static DFManager Instance;

    private List<Seed> _seeds = new List<Seed>();
    private int _triggeredCount = 0;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject winPopupPrefab;
    [SerializeField] private GameObject losePopupPrefab;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject hintPopupPrefab;
    [SerializeField] private float timeLimit = 20f;

    private bool _isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Seed[] allSeeds = FindObjectsOfType<Seed>();
        _seeds.AddRange(allSeeds);

        StartCoroutine(StartCountdown());
    }

    public void MarkSeedAsTriggered(Seed seed)
    {
        if (_isGameOver) return;

        _triggeredCount++;

        if (_triggeredCount >= _seeds.Count)
        {
            Win();
        }
    }

    private void Win()
    {
        if (_isGameOver) return;
        _isGameOver = true;

        if (gameplayPanel != null)
        {
            hintPopupPrefab.SetActive(false);
            gameplayPanel.SetActive(false);
            winPopupPrefab.SetActive(true);
        }
    }   

    private void Lose()
    {
        if (_isGameOver) return;
        _isGameOver = true;

        if (gameplayPanel != null)
        {
            hintPopupPrefab.SetActive(false);
            gameplayPanel.SetActive(false);
            losePopupPrefab.SetActive(true);
            
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
    }

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(timeLimit);

        if (!_isGameOver)
        {
            Lose();
        }
    }

    private void Hint()
    {
        if (hintPopupPrefab != null && !_isGameOver)
        {
            hintPopupPrefab.SetActive(true);
        }
    }
}
