using UnityEngine;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour
{
    [SerializeField] private Button replayButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private GameObject losePopup;

    private void Start()
    {
        if (replayButton != null) replayButton.onClick.AddListener(PlayAgain);
        if (homeButton != null) homeButton.onClick.AddListener(Home);
        
    }

    private void PlayAgain()
    {
        if (losePopup != null) losePopup.SetActive(false);
        LevelManager.Instance?.RestartLevel();
    }

    private void Home()
    {
        if (losePopup != null) losePopup.SetActive(false);
        SelectLevelPanel.Instance?.Home();
    }
}