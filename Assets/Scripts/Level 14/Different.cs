using UnityEngine;
using UnityEngine.UI;

public class Different : MonoBehaviour
{
    [System.Serializable]
    public class DifferencePair
    {
        public Button pointAButton;
        public Button pointBButton;
        public GameObject circleA; 
        public GameObject circleB; 
        public bool isFound = false;
    }

    public DifferencePair[] differencePairs;

    void Start()
    {
        if (FDManager.Instance != null)
        {
            FDManager.Instance.Initialize(differencePairs.Length);
        }
        else
        {
            Debug.LogError("FDManager Instance not found!");
            return;
        }

        foreach (var pair in differencePairs)
        {
            if (pair.pointAButton == null || pair.pointBButton == null)
            {
                Debug.LogError("One of the buttons is missing!", this);
                continue;
            }

            DifferencePair currentPair = pair; 
            pair.pointAButton.onClick.AddListener(() => OnClickPoint(currentPair));
            pair.pointBButton.onClick.AddListener(() => OnClickPoint(currentPair));

            if (pair.circleA != null) pair.circleA.SetActive(false);
            if (pair.circleB != null) pair.circleB.SetActive(false);
        }
    }

    public void OnClickPoint(DifferencePair pair)
    {
        if (!pair.isFound)
        {
            Debug.Log("Clicked on a difference!", pair.pointAButton);
            pair.isFound = true;
            
            pair.pointAButton.gameObject.SetActive(false);
            pair.pointBButton.gameObject.SetActive(false);

            if (pair.circleA != null)
            {
                pair.circleA.SetActive(true);
                Debug.Log("Circle A enabled", pair.circleA);
            }

            if (pair.circleB != null)
            {
                pair.circleB.SetActive(true);
                Debug.Log("Circle B enabled", pair.circleB);
            }

            if (FDManager.Instance != null)
            {
                FDManager.Instance.OnDifferenceFound(); // Chỉ gọi **1 lần** mỗi cặp
            }
        }
        else
        {
            Debug.Log("This difference was already found!", pair.pointAButton);
        }
    }
}
