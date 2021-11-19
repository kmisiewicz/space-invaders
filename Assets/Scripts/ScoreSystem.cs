using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KM.SpaceInvaders
{
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] int scoreBoardSize = 10;


        public static ScoreSystem Instance
        {
            get => _instance;
        }

        static ScoreSystem _instance = null;


        private void Awake()
        {
            //If no instance exists, then assign this instance
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                DestroyImmediate(this);
        }

        public bool SaveScore(int points, out int rank)
        {
            rank = -1;
            return false;
        }

    }
}