using Character;
using UnityEngine;
using WeaponSystem.Data;

public class EnemyController : BaseCharacter
{
    public string nameFlag;


    public override void OnBulletHit(WeaponRuntime weapon)
    {
        base.OnBulletHit(weapon);
        
        Debug.Log($"*** {nameFlag}");
    }
}