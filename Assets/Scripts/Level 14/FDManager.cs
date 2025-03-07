using UnityEngine;

public class FDManager : MonoBehaviour
{
    public static FDManager Instance;

    [SerializeField] private GameObject winPopupPrefab;
    [SerializeField] private GameObject losePopupPrefab;
    [SerializeField] private GameObject gameplayPanel;
    private Canvas canvas; 

    private int totalPairs;
    private int foundPairs;
    private bool _isGameOver = false;
    [SerializeField] float timeLimit = 30f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>(); 
    }

    public void Initialize(int pairCount)
    {
        totalPairs = pairCount*2;
        foundPairs = 0;
        _isGameOver = false;

        Invoke(nameof(LoseGame), timeLimit);
    }

    public void OnDifferenceFound()
    {
        if (_isGameOver) return;

        foundPairs++;

        if (foundPairs >= totalPairs)
        {
            CancelInvoke(nameof(LoseGame));
            WinGame();
        }
    }

    private void WinGame()
    {
        if (_isGameOver) return;
        _isGameOver = true;

        if (gameplayPanel != null) gameplayPanel.SetActive(false);
        if (winPopupPrefab != null) winPopupPrefab.SetActive(true);
    }

    private void LoseGame()
    {
        if (_isGameOver) return;
        _isGameOver = true;

        if (gameplayPanel != null) gameplayPanel.SetActive(false);
        if (losePopupPrefab != null) losePopupPrefab.SetActive(true);
    }
}