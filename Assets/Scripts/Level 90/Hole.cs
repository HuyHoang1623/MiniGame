using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private RectTransform holeUI;
    [SerializeField] private float holeRadius = 30f;
    [SerializeField] private float absorbSpeed = 5f;
    [SerializeField] private Ball ball;

    private bool ballInHole = false;

    void Update()
    {
        if (ballInHole) return;

        Vector2 ballPos = ball.ballUI.anchoredPosition;
        Vector2 holePos = holeUI.anchoredPosition;

        float distance = Vector2.Distance(ballPos, holePos);

        if (distance < holeRadius)
        {
            ball.SetVelocity(Vector2.zero);
            ball.ballUI.anchoredPosition = Vector2.Lerp(ballPos, holePos, absorbSpeed * Time.deltaTime);

            if (distance < 5f)
            {
                ballInHole = true;
                ball.HideBall();
                L90Manager.Instance.EndGame(true); 
            }
        }
    }
}