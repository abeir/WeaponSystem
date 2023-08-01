using System;
using Character;
using WeaponSystem.Controller;

namespace WeaponSystem.Data
{
    
    [Serializable]
    public class WeaponRuntime
    {
        public WeaponBase @base;
        public float lastFiringTime;
        
        public BaseCharacter owner;
        public WeaponController controller;
    }
}