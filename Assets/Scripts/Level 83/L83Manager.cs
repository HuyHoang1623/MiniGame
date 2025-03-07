using UnityEngine;

public class L83Manager : MonoBehaviour
{
    public static L83Manager Instance;

    [SerializeField] private GameObject winPopup;
    [SerializeField] private GameObject losePopup;
    [SerializeField] private GameObject gameplayPanel;
    
    private bool _isGameOver = false;
    private int totalItems;
    private int destroyedItems;
    private float timeLimit = 30f;

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
        Invoke(nameof(LoseGame), timeLimit); // Bắt đầu đếm ngược
    }

    public void RegisterItem()
    {
        totalItems++;
    }

    public void OnItemDestroyed()
    {
        if (_isGameOver) return;

        destroyedItems++;
        if (destroyedItems >= totalItems)
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
        if (winPopup != null) winPopup.SetActive(true);
    }

    private void LoseGame()
    {
        if (_isGameOver) return;
        _isGameOver = true;

        if (gameplayPanel != null) gameplayPanel.SetActive(false);
        if (losePopup != null) losePopup.SetActive(true);
    }
}