using System;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem.Controller;
using Object = UnityEngine.Object;

namespace WeaponSystem
{
    public class WeaponAssembler
    {
        
        private static WeaponAssembler _assembler;
        private Dictionary<string, GameObject> _prefabs = new();


        public static WeaponAssembler Instance
        {
            get
            {
                return _assembler ??= new WeaponAssembler();
            }
        }
        
        /// <summary>
        /// 通过武器预制件名称，加载预制件并创建实例后获取其中的 WeaponController，创建预制件实例后会设置 active 为 false
        /// </summary>
        /// <param name="name">武器预制件名称，通过 Resources 加载 Weapon/Prefabs 路径中的同名预制件</param>
        /// <param name="parent">创能 GameObject 实例时设置的父级</param>
        /// <returns>实例中的 WeaponController 对象</returns>
        /// <exception cref="InvalidOperationException">若对象中没有挂载 WeaponController 脚本将抛出该异常</exception>
        public WeaponController CreateWeapon(string name, Transform parent)
        {
            return CreateFromPrefab<WeaponController>(name, parent, false);
        }

        /// <summary>
        /// 通过子弹预制件名称，加载预制件并创建实例后获取其中的 BulletController，创建预制件实例后会设置 active 为 false
        /// </summary>
        /// <param name="name">子弹预制件名称，通过 Resources 加载 Weapon/Prefabs 路径中的同名预制件</param>
        /// <param name="parent">创能 GameObject 实例时设置的父级</param>
        /// <param name="worldPositionStays">指定父对象时，传递true可直接在世界空间中定位新对象。传递false以设置对象相对于其父对象的位置。</param>
        /// <returns>实例中的 BulletController 对象</returns>
        /// <exception cref="InvalidOperationException">若对象中没有挂载 BulletController 脚本将抛出该异常</exception>
        public BulletController CreateBullet(string name, Transform parent, bool worldPositionStays = false)
        {
            return CreateFromPrefab<BulletController>(name, parent, worldPositionStays);;
        }


        public GameObject LoadPrefab(string name)
        {
            var prefabName = $"Weapon/Prefabs/{name}";
            if (_prefabs.TryGetValue(prefabName, out var prefab))
            {
                return prefab;
            }
            prefab = Resources.Load<GameObject>(prefabName);
            _prefabs[prefabName] = prefab;
            return prefab;
        }

        private T CreateFromPrefab<T>(string name, Transform parent, bool worldPositionStays)
        {
            var prefab = LoadPrefab(name);
            var go = Object.Instantiate(prefab, parent, worldPositionStays);
            
            go.SetActive(false);
            if (!go.TryGetComponent<T>(out var ctrl))
            {
                throw new InvalidOperationException($"Not find {typeof(T).Name} on prefab: {name}");
            }
            return ctrl;
        }
    }
}