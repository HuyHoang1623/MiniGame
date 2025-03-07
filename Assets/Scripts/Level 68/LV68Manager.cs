using System.Collections;
using UnityEngine;

public class LV68Manager : MonoBehaviour
{
    private Card firstCard = null;
    private Card secondCard = null;
    private bool isChecking = false;
    private int totalPairs;
    private int matchedPairs = 0;

    [SerializeField] private GameObject winPopup;
    [SerializeField] private GameObject losePopup;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private float timeLimit = 30f; 

    private bool isGameOver = false;

    private void Start()
    {
        totalPairs = FindObjectsOfType<Card>().Length / 2;

        if (winPopup != null) winPopup.SetActive(false);
        if (losePopup != null) losePopup.SetActive(false);

        StartCoroutine(LoseCountdown());
    }

    public void CardFlipped(Card card)
    {
        if (isGameOver) return;
        
        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null)
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        isChecking = true;
        yield return new WaitForSeconds(0.05f);

        if (firstCard.IsMatching(secondCard))
        {
            matchedPairs++;
            if (matchedPairs >= totalPairs)
            {
                WinGame();
            }
        }
        else
        {
            firstCard.HideCard();
            secondCard.HideCard();
        }

        firstCard = null;
        secondCard = null;
        isChecking = false;
    }

    private IEnumerator LoseCountdown()
    {
        yield return new WaitForSeconds(timeLimit);
        if (!isGameOver)
        {
            LoseGame();
        }
    }

    private void WinGame()
    {
        if (isGameOver) return;
        isGameOver = true;

        gameplayPanel.SetActive(false);
        if (winPopup != null)
            winPopup.SetActive(true);
    }

    private void LoseGame()
    {
        if (isGameOver) return;
        isGameOver = true;

        gameplayPanel.SetActive(false);
        if (losePopup != null)
            losePopup.SetActive(true);
    }

    public bool IsChecking()
    {
        return isChecking;
    }
}
