using Character;
using UnityEngine;
using WeaponSystem.Controller;

public class Bullet1Controller : BulletController
{
    private TrailRenderer _trail;

    private void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
    }

    private void OnDisable()
    {
        _trail.Clear();
    }

    private void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy"))
        {
            return;
        }

        var enemy = other.GetComponent<BaseCharacter>();
        enemy.OnBulletHit(Weapon);
        
        gameObject.SetActive(false);
    }
}