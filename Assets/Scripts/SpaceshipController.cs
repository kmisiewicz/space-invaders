using KM.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace KM.SpaceInvaders
{
    public class SpaceshipController : MonoBehaviour
    {
        [SerializeField] ButtonClickHandler leftButton;
        [SerializeField] ButtonClickHandler rightButton;
        [SerializeField] float moveVelocity = 3f;
        [SerializeField] float maxVelocityChange = 1.5f;

        [SerializeField] Transform shootingPoint;
        [SerializeField, Min(0.1f)]
        [Tooltip("Time in seconds between bullets being shot from player's ship")] 
        float shootingInterval = 2f;

        [SerializeField] EnemiesManager enemiesManager;

        public float ShootingIntervalMultiplier 
        {
            get => _shootingIntervalMultiplier;
            set
            {
                CancelInvoke("ShootBullet");
                _shootingIntervalMultiplier = value;
                ShootBullet();
            } 
        }

        Rigidbody2D _rb;
        float _shootingIntervalMultiplier = 1f;

        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            if (enemiesManager == null)
                enemiesManager = FindObjectOfType<EnemiesManager>();
            Invoke("ShootBullet", shootingInterval);
        }

        private void FixedUpdate()
        {
            MoveSpaceship();
        }

        private void MoveSpaceship()
        {
            Vector2 direction = (leftButton.IsPressed ? Vector2.left : Vector2.zero) +
                                (rightButton.IsPressed ? Vector2.right : Vector2.zero);
            Vector2 targetVelocity = direction * moveVelocity;
            Vector2 velocityChange = targetVelocity - _rb.velocity;
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            _rb.velocity += velocityChange;
        }

        private void ShootBullet()
        {
            BulletManager.Instance?.Shoot(shootingPoint.position, Vector2.up, "PlayerBullet");
            Invoke("ShootBullet", shootingInterval * ShootingIntervalMultiplier);
        }

        public void OnGameOver()
        {
            CancelInvoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
            {
                GameManager.Instance.AdjustPoints(-2 * enemiesManager.Rows);
            }
        }
    }
}