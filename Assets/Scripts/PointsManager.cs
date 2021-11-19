using TMPro;
using UnityEngine;

namespace KM.SpaceInvaders
{
    public class PointsManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;

        public int Points { get; private set; } = 0;


        public void AdjustPoints(int points)
        {
            Points += points;
            scoreText.SetText(Points.ToString());
        }

        private void OnDestroy()
        {

        }
    }
}