using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Data;

namespace WeaponSystem
{
    public class WeaponReleaser
    {
        public WeaponRuntime Current { get; private set; }
        
        public UnityEvent<string> ErrorEvent = new();


        #region 内部变量

        private readonly Dictionary<int, WeaponRuntime> _weaponCache = new();
        private Coroutine _fireCoroutine;

        #endregion

        public WeaponRuntime Select(WeaponBase weapon, BaseCharacter owner)
        {
            if (Current != null && weapon.id == Current.@base.id)
            {
                return Current;
            }

            if (!_weaponCache.TryGetValue(weapon.id, out var weaponRuntime))
            {
                weaponRuntime = new WeaponRuntime()
                {
                    @base = weapon,
                    owner = owner,
                    controller = WeaponAssembler.Instance.CreateWeapon(weapon.weaponPrefab, owner.transform)
                };
                _weaponCache[weapon.id] = weaponRuntime;
            }

            foreach (var w in _weaponCache.Where(w => w.Key != weaponRuntime.@base.id))
            {
                w.Value.controller.gameObject.SetActive(false);
            }
            
            Current = weaponRuntime;
            
            Current.controller.OnSelected(Current);
            
            return weaponRuntime;
        }


        public void Fire()
        {
            if (Current.@base.emissionType == EmissionType.Single)
            {
                SingleFiring();
                return;
            }

            if (_fireCoroutine != null)
            {
                Current.owner.StopCoroutine(_fireCoroutine);
                _fireCoroutine = null;
            }
            _fireCoroutine = Current.owner.StartCoroutine(FireCoroutine());
        }

        public void CancelFire()
        {
            if (Current.@base.emissionType == EmissionType.Continuous && _fireCoroutine != null)
            {
                Current.owner.StopCoroutine(_fireCoroutine);
                _fireCoroutine = null;
            }
            Current.controller.CancelFire(Current);
        }


        private bool Prepare()
        {
            if (Current == null)
            {
                ErrorEvent.Invoke("You must select a weapon first");
                return false;
            }
            if ((Time.time - Current.lastFiringTime) < Current.@base.firingInterval)
            {
                ErrorEvent.Invoke("Weapon cooling down");
                return false;
            }
            if (Current.@base.cost > Current.@base.resource)
            {
                ErrorEvent.Invoke("Not enough " + Current.@base.resourceType);
                return false;
            }
            return true;
        }

        private void SingleFiring()
        {
            if (!Current.owner.gameObject.activeSelf || !Prepare())
            {
                return;
            }
            FireInternal();
        }

        private IEnumerator FireCoroutine()
        {
            var waitFiringInterval = new WaitForSeconds(Current.@base.firingInterval);

            while (Current.owner.gameObject.activeSelf && Prepare())
            {
                FireInternal();
                yield return waitFiringInterval;
            }
        }

        private void FireInternal()
        {
            Current.lastFiringTime = Time.time;
            Current.@base.resource -= Current.@base.cost;
            
            Current.controller.Fire(Current);
        }
    }
}