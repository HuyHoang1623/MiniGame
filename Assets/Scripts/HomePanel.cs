using UnityEngine;
using UnityEngine.UI;

public class HomePanel : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject lockedScreen;  
    [SerializeField] private GameObject unlockedScreenPrefab; 
    [SerializeField] private Transform canvasParent; 
    [SerializeField] private float unlockThreshold = 0.98f;

    private bool isUnlocked = false;

    private void Start()
    {
        if (slider != null)
        {
            slider.onValueChanged.AddListener(CheckUnlock);
            slider.value = 0;
        }

        if (lockedScreen != null)
            lockedScreen.SetActive(true);
    }

    private void CheckUnlock(float value)
    {
        if (!isUnlocked && value >= unlockThreshold)
        {
            Unlock();
        }
    }

    private void Unlock()
    {
        isUnlocked = true;
        slider.interactable = false;

        if (lockedScreen != null)
            lockedScreen.SetActive(false);

        if (unlockedScreenPrefab != null && canvasParent != null)
        {
            GameObject unlockedScreen = Instantiate(unlockedScreenPrefab, canvasParent);
            unlockedScreen.transform.SetAsLastSibling();
        }

    }
}