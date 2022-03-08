using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : MonoBehaviour
{
    private Health _health;
    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void InflictDamage(float damage)
    {   if(_health.health > 0)
        {
            if (damage > _health.health)
            {

                _health.TakeDamage(_health.health);
            }
            else
            {
                _health.TakeDamage(damage);
            }
            
        }
    }
}
