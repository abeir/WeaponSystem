using Sirenix.Serialization;
using UnityEngine;
using WeaponSystem.Data;

namespace WeaponSystem.Loader
{
    public class JsonWeaponLoader : IWeaponLoader
    {
        
        
        public WeaponConfig Load()
        {
            var jsonFile = Resources.Load<TextAsset>("Weapon/Configs/Weapons");
            var config = SerializationUtility.DeserializeValue<WeaponConfig>(System.Text.Encoding.UTF8.GetBytes(jsonFile.text), DataFormat.JSON);
            Resources.UnloadAsset(jsonFile);
            return config;
        }

    }
}