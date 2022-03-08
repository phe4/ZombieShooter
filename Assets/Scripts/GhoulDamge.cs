using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulDamge : MonoBehaviour
{
    //private Collider myColLastHit = null;
    private void OnTriggerEnter(Collider col)
    {
        //if (col.GetComponent<IlastCollide>())
        //{
        //    myColLastHit = col.GetComponent<IlastCollide>().iLastEntered;
        //}
        
        PlayerDamageable damageable = col.GetComponent<PlayerDamageable>();
        if (damageable && GetComponent<EnemyController>().state == "attacking")
        {
            damageable.InflictDamage(5.0f);
            DI_System.CreateIndicator(this.transform);
        }
    }
}
