using UnityEngine;

namespace Pool
{
    public class PoolManager
    {
        private static PoolManager _poolManager;


        public static PoolManager Instance
        {
            get
            {
                return _poolManager ??= new PoolManager();
            }
        }


        private PoolManager()
        {
        }

        public PrefabPool CreatePrefabPool(GameObject origin, Transform parent, bool worldPositionStays = false)
        {
            var go = new GameObject(origin.name + "_pool");
            go.SetActive(false);
            var pool = go.AddComponent<PrefabPool>();
            pool.SetPrefab(origin, parent, worldPositionStays);
            return pool;
        }
        
        public PrefabPool CreatePrefabPool(GameObject origin, Vector3 position, Quaternion quaternion)
        {
            var go = new GameObject(origin.name + "_pool");
            go.SetActive(false);
            var pool = go.AddComponent<PrefabPool>();
            pool.SetPrefab(origin, position, quaternion);
            return pool;
        }
        
    }
}