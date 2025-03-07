using UnityEngine;

public class SelectLevelPanel : MonoBehaviour
{
    public static SelectLevelPanel Instance { get; private set; }
    public GameObject[] uiPrefabs;

    private Transform canvasTransform;
    private GameObject currentLevel;
    private int currentLevelIndex = -1;

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

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            canvasTransform = canvas.transform;
        }
    }

    public void SpawnUIPrefab(int index)
    {
        if (canvasTransform == null)
        {
            return;
        }

        if (uiPrefabs == null || uiPrefabs.Length == 0 || index < 0 || index >= uiPrefabs.Length || uiPrefabs[index] == null)
        {
            return;
        }

        if (currentLevel != null)
        {
            DestroyImmediate(currentLevel);
        }

        currentLevel = Instantiate(uiPrefabs[index], canvasTransform);
        currentLevel.SetActive(true);
        currentLevelIndex = index; 

        if (currentLevel.TryGetComponent(out RectTransform rectTransform))
        {
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }

        Debug.Log($"Spawned UI Level {index}: {currentLevel.name}");
        gameObject.SetActive(false);
    }

    public void Home()
    {
        if (currentLevel != null)
        {
            DestroyImmediate(currentLevel);
            currentLevel = null;
        }

        gameObject.SetActive(true);
    }

    public int GetCurrentLevelIndex()
    {
        return currentLevelIndex; 
    }
}
