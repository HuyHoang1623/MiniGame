using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardID;
    public GameObject frontSide;
    public GameObject backSide;
    private bool isFlipped = false;
    private LV68Manager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<LV68Manager>();
        SetCardState(false);
    }

    public void OnCardClicked()
    {
        if (isFlipped || gameManager.IsChecking()) return;

        FlipCard();
        gameManager.CardFlipped(this);
    }

    public void FlipCard()
    {
        isFlipped = !isFlipped;
        SetCardState(isFlipped);
    }

    private void SetCardState(bool showFront)
    {
        frontSide.SetActive(showFront);
        backSide.SetActive(!showFront);
    }

    public bool IsMatching(Card otherCard)
    {
        return this.cardID == otherCard.cardID;
    }

    public void HideCard()
    {
        StartCoroutine(HideCardAfterDelay());
    }

    private IEnumerator HideCardAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        FlipCard();
    }
}