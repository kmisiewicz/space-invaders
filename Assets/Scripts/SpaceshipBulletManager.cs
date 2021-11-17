using UnityEngine;
using UnityEngine.Pool;

namespace KM.SpaceInvaders
{
    public class SpaceshipBulletManager : MonoBehaviour
    {
        [SerializeField] BulletBehaviour bulletPrefab;
        [SerializeField] Transform bulletParent;


        ObjectPool<BulletBehaviour> _bulletPool;


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

        public void Shoot(Vector3 startPosition, string layerName)
        {
            var bullet = _bulletPool.Get();
            bullet.Initialize(BulletHit, startPosition, layerName);
        }

        public void BulletHit(BulletBehaviour bullet) => _bulletPool.Release(bullet);
    }
}
