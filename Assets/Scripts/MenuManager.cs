using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KM.SpaceInvaders
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] string gameSceneName = "SpaceInvaders";
        [SerializeField] TextMeshProUGUI topScores;


        public void LoadGame()
        {
            SceneManager.LoadScene(gameSceneName);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        private void Start()
        {
            LoadScoreboard();
        }

        private void LoadScoreboard()
        {
            int?[] scores = ScoreSystem.Instance.GetScoreboard();
            StringBuilder scoreBoardBuilder = new StringBuilder();
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] != null)
                    scoreBoardBuilder.Append($"\n{i + 1}. {scores[i]}");
                else
                    scoreBoardBuilder.Append($"\n{i + 1}. -");
            }
            topScores.SetText(scoreBoardBuilder.ToString());
        }
    }
}