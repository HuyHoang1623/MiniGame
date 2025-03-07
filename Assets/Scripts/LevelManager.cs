using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private GameObject[] uiPrefabs;
    private int currentIndex = 0;

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
            return;
        }

        if (uiPrefabs == null || uiPrefabs.Length == 0)
        {
        }
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    public void StartLevel(int index)
    {
        if (index >= 0 && index < uiPrefabs.Length)
        {
            currentIndex = index;
            SpawnLevel();
        }
        else
        {
            Debug.LogError($"Invalid level index: {index}");
        }
    }

    public void RestartLevel()
    {
        if (SelectLevelPanel.Instance != null)
        {
            currentIndex = SelectLevelPanel.Instance.GetCurrentLevelIndex();
        }

        Debug.Log($"Restarting level: {currentIndex}");
        SpawnLevel();
    }

    public void NextLevel()
    {
        if (currentIndex + 1 < uiPrefabs.Length)
        {
            currentIndex++;
            SpawnLevel();
        }
        else
        {
            GoHome();
        }
    }

    public void GoHome()
    {
        currentIndex = 0;
        SelectLevelPanel.Instance?.Home();
    }

    private void SpawnLevel()
    {
        if (SelectLevelPanel.Instance != null)
        {
            SelectLevelPanel.Instance.SpawnUIPrefab(currentIndex);
        }

    }
}
