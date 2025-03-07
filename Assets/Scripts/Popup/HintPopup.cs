using UnityEngine;
using UnityEngine.UI;

public class HintPopup : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        if (popup == null)
        {
            Debug.LogWarning("Popup GameObject is not assigned in HintPopup.");
        }
        else
        {
            popup.SetActive(false);
        }

        if (closeButton == null)
        {
            Debug.LogWarning("Close Button is not assigned in HintPopup.");
        }
        else
        {
            closeButton.onClick.AddListener(ClosePopup);
        }
    }

    public void ShowPopup()
    {
        if (popup != null)
        {
            popup.SetActive(true);
        }
    }

    private void ClosePopup()
    {
        if (popup != null)
        {
            popup.SetActive(false);
        }
    }
}