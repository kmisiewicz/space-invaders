using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KM.SpaceInvaders
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] SpaceshipController playerShip;
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI gameOverText;
        [SerializeField] Canvas gameOverCanvas;
        [SerializeField] Canvas moveButtonsCanvas;
        [SerializeField] string menuScene = "MainMenu";

        public int Points { get; private set; } = 0;

        public static GameManager Instance
        {
            get => _instance;
        }

        static GameManager _instance = null;


        private void Awake()
        {
            //If no instance exists, then assign this instance
            if (_instance == null)
                _instance = this;
            else
                DestroyImmediate(this);
        }


        public void AdjustPoints(int points)
        {
            Points += points;
            scoreText.SetText(Points.ToString());
        }

        public void GameOver(string outcome)
        {
            // Disable buttons moving the ship
            moveButtonsCanvas.enabled = false;
            playerShip.OnGameOver();

            gameOverText.SetText($"Score: {Points}\nYou {outcome}");
            // Detemine if new score fits on scoreboard
            if (ScoreSystem.Instance.SubmitScore(Points, out int rank))
            {
                gameOverText.SetText(gameOverText.text + $"\nYour score is top {rank} on the leaderboard!");
            }

            // Show game over screen
            gameOverCanvas.enabled = true;
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(menuScene);
        }
    }
}