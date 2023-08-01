using System;
using UnityEngine;

namespace WeaponSystem.Data
{
    
    [Serializable]
    public class WeaponBase
    {
        public int id;
        public string name;
        [TextArea(4, 10)]
        public string description;
        public string icon;
        
        public float cost;
        public float resource;
        public ResourceType resourceType;

        public float moveSpeed;
        public Vector3 moveDirection;
        
        public float damage;
        public float damageDuration;
        public float firingInterval;
        public EmissionType emissionType;

        public string weaponPrefab;
        public string bulletPrefab;
    }
}