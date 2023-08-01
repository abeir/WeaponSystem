using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using WeaponSystem;
using WeaponSystem.Data;

namespace Character
{
    public class BaseCharacter : MonoBehaviour
    {
        [ShowInInspector, Required] 
        public CharacterResource characterResource;

        
        public bool enableWeaponSystem;

        [CanBeNull]
        protected WeaponManager WeaponManager;

        protected virtual void Awake()
        {
            if (enableWeaponSystem)
            {
                WeaponManager = new WeaponManager(this);
                WeaponManager.AddListenError(OnWeaponManagerError);
            }
        }

        protected virtual void OnEnable()
        {
            if (enableWeaponSystem && WeaponManager?.Current == null)
            {
                var defaultWeapon = WeaponFactory.Instance.DefaultWeapon;
                WeaponManager?.Select(defaultWeapon.id);
            }
        }

        protected virtual void OnDestroy()
        {
            WeaponManager?.RemoveListenError();
        }


        public virtual void OnBulletHit(WeaponRuntime weapon)
        {
            
            Debug.Log($"**** OnBulletHit: {weapon.@base.name} -> {gameObject.name}");
        }

        public virtual void OnWeaponManagerError(string err)
        {
            Debug.Log($"xxxxxx OnWeaponManagerError: {err}");
        }
    }
}