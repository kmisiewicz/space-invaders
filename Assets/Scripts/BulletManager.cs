using UnityEngine;
using UnityEngine.Pool;

namespace KM.SpaceInvaders
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] BulletBehaviour bulletPrefab;
        [SerializeField] Transform bulletParent;

        public static BulletManager Instance
        {
            get => _instance;
        }

        static BulletManager _instance = null;
        ObjectPool<BulletBehaviour> _bulletPool;


        private void Awake()
        {
            //If no instance exists, then assign this instance
            if (_instance == null)
                _instance = this;
            else
                DestroyImmediate(this);
        }

        private void Start()
        {
            _bulletPool = new ObjectPool<BulletBehaviour>(() => {
                return Instantiate(bulletPrefab, bulletParent);
            }, bullet => {
                bullet.gameObject.SetActive(true);
            }, bullet => {
                bullet.gameObject.SetActive(false);
            }, bullet => {
                Destroy(bullet.gameObject);
            }, true, 10, 20);
        }

        public void Shoot(Vector3 startPosition, Vector2 direction, string layerName)
        {
            var bullet = _bulletPool.Get();
            bullet.Initialize(BulletHit, startPosition, direction, layerName);
        }

        public void BulletHit(BulletBehaviour bullet) => _bulletPool.Release(bullet);
    }
}
