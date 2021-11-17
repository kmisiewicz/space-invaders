using KM.Utility;
using UnityEngine;

namespace KM.SpaceInvaders
{
    public class SpaceshipController : MonoBehaviour
    {
        [SerializeField] ButtonClickHandler leftButton;
        [SerializeField] ButtonClickHandler rightButton;
        [SerializeField] float moveVelocity = 3f;
        [SerializeField] float maxVelocityChange = 1.5f;

        [SerializeField] SpaceshipBulletManager bulletManager;
        [SerializeField] Transform shootingPoint;
        [SerializeField, Min(0.1f)]
        [Tooltip("Time in seconds between bullets being shot from player's ship")] 
        float shootingInterval = 2f;


        Rigidbody2D _rb;

        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            InvokeRepeating("ShootBullet", shootingInterval, shootingInterval);
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
            bulletManager?.Shoot(shootingPoint.position, "PlayerBullet");
        }
    }
}