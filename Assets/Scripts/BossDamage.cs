using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamge : MonoBehaviour
{
    //private Collider myColLastHit = null;
    private void OnTriggerEnter(Collider col)
    {
        //if (col.GetComponent<IlastCollide>())
        //{
        //    myColLastHit = col.GetComponent<IlastCollide>().iLastEntered;
        //}
        
        PlayerDamageable damageable = col.GetComponent<PlayerDamageable>();
        if (damageable && GetComponent<BossController>().state == "attacking")
        {
            damageable.InflictDamage(50.0f);
            DI_System.CreateIndicator(this.transform);
        }
    }
}