using System;
using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    
    [Serializable, CreateAssetMenu(menuName = "配置/角色资源")]
    public class CharacterResource : ScriptableObject
    {
        public float health;
        public float currentHealth;
        public float energy;
        public float currentEnergy;
        public float moveSpeed;
        public float currentMoveSpeed;


        public UnityAction HealthEmptyAction;
        public UnityAction EnergyEmptyAction;

        public float ChangeCurrentHealth(float value)
        {
            currentHealth = Mathf.Clamp(currentHealth + value, 0, health);
            if (currentHealth <= Mathf.Epsilon)
            {
                HealthEmptyAction?.Invoke();
            }
            return currentHealth;
        }
        
        public float ChangeCurrentEnergy(float value)
        {
            currentEnergy = Mathf.Clamp(currentEnergy + value, 0, health);
            if (currentEnergy <= Mathf.Epsilon)
            {
                EnergyEmptyAction?.Invoke();
            }
            return currentEnergy;
        }

        public float ChangeCurrentMoveSpeed(float value)
        {
            currentMoveSpeed = Mathf.Clamp(currentMoveSpeed + value, 0, health);
            return currentMoveSpeed;
        }
        
    }
}