using KM.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace KM.SpaceInvaders
{
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] int scoreBoardSize = 10;
        [SerializeField, SimpleButton("ClearScores")] bool clearScoresButton;


        public static ScoreSystem Instance
        {
            get => _instance;
        }

        static ScoreSystem _instance = null;
        List<int?> topScores;


        private void Awake()
        {
            //If no instance exists, then assign this instance
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                LoadScores();
            }
            else
                DestroyImmediate(this);
        }

        private void LoadScores()
        {
            topScores = new List<int?>();
            for (int i = 0; i < scoreBoardSize; i++)
            {
                string key = $"Score{i + 1}";
                if (PlayerPrefs.HasKey(key))
                    topScores.Add(PlayerPrefs.GetInt(key));
                else
                    topScores.Add(null);
            }
        }

        public bool SubmitScore(int points, out int rank)
        {
            rank = -1;

            for (int i = 0; i < scoreBoardSize; i++)
            {
                if (topScores[i] == null || points > topScores[i])
                {
                    rank = i + 1;
                    topScores.Insert(i, points);
                    topScores.RemoveAt(scoreBoardSize);
                    return true;
                }
            }

            return false;
        }

        private void OnApplicationQuit()
        {
            SaveScores();
        }

        private void SaveScores()
        {
            for (int i = 0; i < scoreBoardSize; i++)
            {
                if (topScores[i] == null)
                    break;

                PlayerPrefs.SetInt($"Score{i + 1}", (int)topScores[i]);
            }
        }

        public int?[] GetScoreboard()
        {
            return topScores.ToArray();
        }

        public void ClearScores()
        {
            if (topScores != null)
                topScores.ForEach(score => score = null);
            PlayerPrefs.DeleteAll();
        }
    }
}