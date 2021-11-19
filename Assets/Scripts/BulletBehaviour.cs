using System;
using UnityEngine;

namespace KM.SpaceInvaders
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] float bulletSpeed;

        Action<BulletBehaviour> _hitAction;
        Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Initialize(Action<BulletBehaviour> hitAction, Vector3 startPosition, Vector2 direction, string layerName)
        {
            _hitAction = hitAction;
            gameObject.layer = LayerMask.NameToLayer(layerName);
            transform.position = startPosition;
            _rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D collision) => _hitAction(this);
    }
}