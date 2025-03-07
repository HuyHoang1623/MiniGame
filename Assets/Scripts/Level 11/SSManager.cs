using UnityEngine;
using System;
using System.Collections;

namespace SupermarketSort
{
    public class SSManager : MonoBehaviour
    {
        public static SSManager Instance { get; private set; }

        [SerializeField] private ItemSlot[] itemSlots;
        [SerializeField] private GameObject winPopupPrefab;
        [SerializeField] private GameObject gameplayPanel;
        [SerializeField] private GameObject gameLosePanel;
        [SerializeField] private float timeLimit = 30f;

        private Canvas canvas;
        private bool _isGameOver = false;
        private Coroutine loseCountdownCoroutine;

        public static event Action OnWin;
        public static event Action OnLose;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            FindCanvas();
            StartLoseCountdown();
        }

        private void FindCanvas()
        {
            canvas = FindObjectOfType<Canvas>();
        }

        private void StartLoseCountdown()
        {
            if (loseCountdownCoroutine != null)
            {
                StopCoroutine(loseCountdownCoroutine);
            }
            loseCountdownCoroutine = StartCoroutine(LoseCountdown());
        }

        public void CheckWinCondition()
        {
            if (_isGameOver) return;

            foreach (var slot in itemSlots)
            {
                if (!slot.IsFilled)
                    return;
            }

            WinGame();
        }

        private void WinGame()
        {
            if (_isGameOver) return;
            _isGameOver = true;

            if (loseCountdownCoroutine != null)
            {
                StopCoroutine(loseCountdownCoroutine);
                loseCountdownCoroutine = null;
            }

            gameplayPanel?.SetActive(false);

            if (winPopupPrefab != null)
            {
                winPopupPrefab.SetActive(true);  
            }

            OnWin?.Invoke();
        }

        private void LoseGame()
        {
            if (_isGameOver) return;
            _isGameOver = true;

            gameplayPanel?.SetActive(false);
            gameLosePanel?.SetActive(true);

            OnLose?.Invoke();
        }

        private IEnumerator LoseCountdown()
        {
            yield return new WaitForSeconds(timeLimit);
            if (!_isGameOver)
            {
                LoseGame();
            }
        }
    }
}
