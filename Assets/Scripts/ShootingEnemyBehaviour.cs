using UnityEngine;

namespace KM.SpaceInvaders
{
    public class ShootingEnemyBehaviour : BasicEnemyBehaviour
    {
        [Header("Shooting")]

        [SerializeField] Transform bulletOrigin;

        [SerializeField, Min(0.1f)]
        [Tooltip("Time in seconds between bullets being shot from player's ship")]
        float minShootingInterval = 4f;

        [SerializeField, Min(0.1f)]
        [Tooltip("Time in seconds between bullets being shot from player's ship")]
        float maxShootingInterval = 7f;

        [SerializeField] float onHitShotIntervalMultiplier = 0.97f;


        float _shootingInterval;


        private void Awake()
        {
            _shootingInterval = Random.Range(minShootingInterval, maxShootingInterval);
            Invoke("ShootBullet", _shootingInterval);
        }

        private void ShootBullet()
        {
            BulletManager.Instance?.Shoot(bulletOrigin.position, Vector2.down, "EnemyBullet");
            Invoke("ShootBullet", _shootingInterval);
        }

        public override void Deactivate()
        {
            CancelInvoke();
        }

        public override void OnOtherEnemiesHit()
        {
            _shootingInterval *= onHitShotIntervalMultiplier;
        }
    }
}