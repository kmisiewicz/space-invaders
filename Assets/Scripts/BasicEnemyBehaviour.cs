using UnityEngine;

namespace KM.SpaceInvaders
{
    public class BasicEnemyBehaviour : MonoBehaviour
    {
        public int Row { get; private set; } = 0;
        public int Column { get; private set; } = 0;


        public void Initialize(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}