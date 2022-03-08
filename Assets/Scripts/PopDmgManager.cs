using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopDmgManager : MonoBehaviour
{
    #region Singleton
    public static PopDmgManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] GameObject damagePopPrefab;
    public void DisplayDmg(int amount, Transform enemy)
    {
        GameObject go = Instantiate(damagePopPrefab, enemy.transform.position, Quaternion.identity, enemy);
        go.GetComponent<PopDmg>().setUp(amount);
    }
    
}
