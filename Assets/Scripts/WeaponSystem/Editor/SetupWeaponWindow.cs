using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace WeaponSystem.Editor
{
    public class SetupWeaponWindow : OdinMenuEditorWindow
    {
        
        [MenuItem("Tools/技能系统/配置技能")]
        public static void CreateSetupSkillWindow()
        {
            var win = GetWindow<SetupWeaponWindow>();
            win.titleContent = new GUIContent("配置武器");
            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.MenuItems.Add(new OdinMenuItem(tree, "武器", new WeaponListMenu()));
            return tree;
        }
    }
}