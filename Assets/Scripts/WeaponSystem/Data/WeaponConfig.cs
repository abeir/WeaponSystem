using System;
using System.Collections.Generic;

namespace WeaponSystem.Data
{
    [Serializable]
    public class WeaponConfig
    {
        public List<WeaponBase> weapons;

        public int defaultWeapon;

        public bool loopSelect;
    }
}