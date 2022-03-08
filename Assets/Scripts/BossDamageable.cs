using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageable : MonoBehaviour
{
    private BossController boss;

    public GameObject getAncestor(GameObject item)
    {
        while(item.transform.parent.name != "Enemy")
        {
            item = item.transform.parent.gameObject;
        }
        return item;
    }

    private void Start()
    {
        GameObject enemyObject = getAncestor(this.gameObject);
        boss = enemyObject.GetComponent<BossController>();
    }
    public void InflictDamage(float damage)
    {
        boss.TakeDamage(damage);
    }
}