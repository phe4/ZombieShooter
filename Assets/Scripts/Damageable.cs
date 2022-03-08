using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private EnemyController enemy;

    public GameObject getAncestor(GameObject item)
    {
        while(item.transform.parent.name != "Enemy")
        {
            item = item.transform.parent.gameObject;
        }
        return item;
    }

    private void Awake()
    {
        GameObject enemyObject = getAncestor(this.gameObject);
        enemy = enemyObject.GetComponent<EnemyController>();
    }
    public void InflictDamage(float damage)
    {
        enemy.TakeDamage(damage);
    }
}
