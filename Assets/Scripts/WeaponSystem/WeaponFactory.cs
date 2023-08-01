using System.Collections.Generic;
using WeaponSystem.Data;
using WeaponSystem.Loader;

namespace WeaponSystem
{
    public class WeaponFactory
    {

        private static WeaponFactory _weaponFactory;
        private WeaponConfig _weaponConfig;
        
        public WeaponBase DefaultWeapon { get; private set; }
        
        public static WeaponFactory Instance => _weaponFactory ??= Create();

        private static WeaponFactory Create()
        {
            _weaponFactory = new WeaponFactory();
            return _weaponFactory;
        }


        private WeaponFactory()
        {
            _weaponConfig = new JsonWeaponLoader().Load();

            DefaultWeapon = _weaponConfig.weapons.Find(w => _weaponConfig.defaultWeapon == w.id);
        }


        public List<WeaponBase> Weapons()
        {
            return _weaponConfig.weapons;
        }

        public bool LoopSelect()
        {
            return _weaponConfig.loopSelect;
        }

    }
}