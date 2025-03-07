using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [SerializeField] private Button homeButton;
    [SerializeField] private Button nextGameButton;
    [SerializeField] private Button playAgainButton;

    private void Start()
    {
        if(homeButton != null) homeButton.onClick.AddListener(Home);
        if(nextGameButton != null) nextGameButton.onClick.AddListener(NextGame);
        if(playAgainButton != null) playAgainButton.onClick.AddListener(PlayAgain);
    }

    private void Home()
    {
        SelectLevelPanel.Instance?.Home();
    }

    private void NextGame()
    {
        LevelManager.Instance.NextLevel();
    }

    private void PlayAgain()
    {
        LevelManager.Instance.RestartLevel();
    }
}
