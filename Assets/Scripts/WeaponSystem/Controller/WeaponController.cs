using Pool;
using UnityEngine;
using WeaponSystem.Data;

namespace WeaponSystem.Controller
{
    public class WeaponController : MonoBehaviour
    {
        
        public float lifetime = 2;
        public int preloadAmount = 5;
        public float detectLifetimeInterval = 0.5f;
        
        
        private PrefabPool _pool;
        
        [SerializeField]
        private WeaponRuntime weapon;

        public virtual void OnSelected(WeaponRuntime weaponRuntime)
        {
            this.weapon = weaponRuntime;
            gameObject.SetActive(true);

            InitPool(weaponRuntime);
        }


        public void Fire(WeaponRuntime weaponRuntime)
        {
            Debug.Log($">>> Fire: {weaponRuntime.@base.name}");

            _pool.Spawn();
        }

        public void CancelFire(WeaponRuntime weaponRuntime)
        {
            Debug.Log($">>> FiringCancel: {weaponRuntime.@base.name}");
        }


        private void InitPool(WeaponRuntime weaponRuntime)
        {
            if (_pool != null)
            {
                return;
            }
            var prefab = WeaponAssembler.Instance.LoadPrefab(weaponRuntime.@base.bulletPrefab);
            var bullet = prefab.GetComponent<BulletController>();
            bullet.speed = weaponRuntime.@base.moveSpeed;
            bullet.direction = weaponRuntime.@base.moveDirection;
            
            _pool = PoolManager.Instance.CreatePrefabPool(prefab, transform.position, Quaternion.identity);
            _pool.CreateAction += OnCreateBullet;
            _pool.lifetime = lifetime;
            _pool.preloadAmount = preloadAmount;
            _pool.detectLifetimeInterval = detectLifetimeInterval;
            _pool.Preload();
            _pool.gameObject.SetActive(true);
        }

        private void OnCreateBullet(GameObject bullet)
        {
            var ctrl = bullet.GetComponent<BulletController>();
            ctrl.Weapon = weapon;
        }

    }
}