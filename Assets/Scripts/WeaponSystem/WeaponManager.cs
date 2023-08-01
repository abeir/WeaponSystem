using System;
using System.Collections.Generic;
using System.Linq;
using Character;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using WeaponSystem.Data;

namespace WeaponSystem
{
    public class WeaponManager
    {
        [ShowInInspector]
        public WeaponBase Current { get; private set; }
        [ShowInInspector]
        public bool LoopSelect { get; private set; }


        #region 内部变量

        private readonly Dictionary<int, WeaponBase> _weapons = new();
        private readonly List<int> _ids = new();


        private WeaponReleaser _releaser = new();
        private BaseCharacter _owner;

        #endregion

        
        public WeaponManager(BaseCharacter owner)
        {
            _owner = owner;
            
            LoopSelect = WeaponFactory.Instance.LoopSelect();
            var weaponList = WeaponFactory.Instance.Weapons();
            AddRange(weaponList);
        }
        

        [CanBeNull]
        public WeaponBase Select(int id)
        {
            if (!_weapons.TryGetValue(id, out var weapon))
            {
                return null;
            }
            Current = weapon;

            _releaser.Select(Current, _owner);
            return weapon;
        }

        [CanBeNull]
        public WeaponBase SelectNext()
        {
            var index = _ids.FindIndex(id => id == Current.id);
            if (index < 0)
            {
                return null;
            }
            index = LoopSelect ? ((index + 1) % _ids.Count) : (Math.Clamp(index + 1, 0, _ids.Count - 1));
            Current = _weapons[_ids[index]];
            _releaser.Select(Current, _owner);
            return Current;
        }

        [CanBeNull]
        public WeaponBase SelectPrev()
        {
            var index = _ids.FindIndex(id => id == Current.id);
            if (index < 0)
            {
                return null;
            }

            int id;
            if (LoopSelect)
            {
                id = index == 0 ? _ids.Last() : _ids[(index - 1) % _ids.Count];
            }
            else
            {
                id = _ids[Math.Clamp(index - 1, 0, _ids.Count - 1)];
            }
            Current = _weapons[id];
            _releaser.Select(Current, _owner);
            return Current;
        }


        public void Add(WeaponBase weapon)
        {
            _weapons[weapon.id] = weapon;
            _ids.Add(weapon.id);
        }

        public void AddRange(List<WeaponBase> weapons)
        {
            foreach (var w in weapons)
            {
                _weapons[w.id] = w;
                _ids.Add(w.id);
            }
        }

        public void Change(int id, WeaponBase weapon)
        {
            var index = _ids.FindIndex(i => i == id);
            if (index < 0)
            {
                return;
            }
            _weapons[weapon.id] = weapon;
            _ids[index] = weapon.id;
        }

        public void Remove(int id)
        {
            _weapons.Remove(id);
            _ids.Remove(id);
        }

        public void RemoveAll()
        {
            _weapons.Clear();
            _ids.Clear();
        }


        public void Fire()
        {
            _releaser.Fire();
        }

        public void CancelFire()
        {
            _releaser.CancelFire();
        }

        public void AddListenError(UnityAction<string> handler)
        {
            _releaser.ErrorEvent.AddListener(handler);
        }

        public void RemoveListenError()
        {
            _releaser.ErrorEvent.RemoveAllListeners();
        }
    }
}