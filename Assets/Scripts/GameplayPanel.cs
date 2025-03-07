using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel : MonoBehaviour
{
    [SerializeField] private Button nextGameButton;
    [SerializeField] private Button homeGameButton;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button hintButton;
    [SerializeField] private GameObject hintPopup;

    private void Start()
    {
        if (nextGameButton != null) nextGameButton.onClick.AddListener(NextGame);
        if (homeGameButton != null) homeGameButton.onClick.AddListener(HomeGame);
        if (playAgainButton != null) playAgainButton.onClick.AddListener(PlayAgain);
        if (hintButton != null) hintButton.onClick.AddListener(ShowHint);

        if (hintPopup != null) hintPopup.SetActive(false);
    }

    private void NextGame()
    {
        if (LevelManager.Instance != null)
        {
            Time.timeScale = 1;
            LevelManager.Instance.NextLevel();
        }
    }

    private void HomeGame()
    {
        SelectLevelPanel.Instance?.Home();
    }

    private void PlayAgain()
    {
        if (LevelManager.Instance != null)
        {
            Time.timeScale = 1;
            LevelManager.Instance.RestartLevel();
        }
    }

    private void ShowHint()
    {
        if (hintPopup != null) hintPopup.SetActive(true);
    }
}