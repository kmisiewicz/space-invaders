using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KM.SpaceInvaders
{
    public class ShootingEnemyBehaviour : BasicEnemyBehaviour
    {
        [Header("Shooting")]
        [SerializeField] SpaceshipBulletManager bulletManager;
        [SerializeField] Transform bulletOrigin;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}