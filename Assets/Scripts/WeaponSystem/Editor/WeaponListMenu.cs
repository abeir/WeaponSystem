#pragma warning disable CS0414
using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using WeaponSystem.Data;
using SerializationUtility = Sirenix.Serialization.SerializationUtility;

namespace WeaponSystem.Editor
{
    public class WeaponListMenu
    {
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [BoxGroup("文件路径"), ReadOnly]
        public string JsonPath;

        public bool LoopSelect;

        [ValueDropdown("SelectWeaponList")]
        public int DefaultWeapon;
            
        [Title("武器列表")]
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 20)]
        public List<WeaponBase> Weapons;
            
        
        [NonSerialized]
        private bool _done;
        [NonSerialized]
        private string _msg;
        
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [Button("生成武器数据", ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1)]
        [InfoBox("$_msg", "_done")]
        private void GenerateButton()
        {
            if (File.Exists(JsonPath))
            {
                if (!EditorUtility.DisplayDialog("生成武器数据", "已存在文件，点击确定进行覆盖", "确定", "取消"))
                {
                    return;
                }
            }

            var config = new WeaponConfig
            {
                weapons = Weapons,
                defaultWeapon = DefaultWeapon,
                loopSelect = LoopSelect
            };

            var json = SerializationUtility.SerializeValue(config, DataFormat.JSON);
            File.WriteAllText(JsonPath, System.Text.Encoding.UTF8.GetString(json));
            
            AssetDatabase.Refresh();
            
            _done = true;
            _msg = "已生成武器数据：" + JsonPath;
        }
            
        public WeaponListMenu()
        {
            JsonPath = Path.GetFullPath("Assets/Resources/Weapon/Configs/Weapons.json");

            if (!File.Exists(JsonPath))
            {
                return;
            }

            var json = File.ReadAllText(JsonPath);
            var config = SerializationUtility.DeserializeValue<WeaponConfig>(System.Text.Encoding.UTF8.GetBytes(json),
                DataFormat.JSON);
            Weapons = config.weapons;
            DefaultWeapon = config.defaultWeapon;
            LoopSelect = config.loopSelect;
        }

        private IEnumerable<ValueDropdownItem<int>> SelectWeaponList()
        {
            var ls = new ValueDropdownList<int>();
            if (Weapons == null)
            {
                return ls;
            }
            foreach (var w in Weapons)
            {
                ls.Add(w.name, w.id);   
            }

            return ls;
        }
    }
}
#pragma warning restore CS0414
