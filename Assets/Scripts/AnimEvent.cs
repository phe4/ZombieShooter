using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void showmsg()
    {
        GetComponent<EnemyController>().state = "run";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
