using UnityEngine;
using WeaponSystem.Data;

namespace WeaponSystem.Controller
{
    
    public class BulletController : MonoBehaviour
    {

        public float speed;
        public Vector3 direction;


        public WeaponRuntime Weapon { get; set; }
     
        
        
    }
}