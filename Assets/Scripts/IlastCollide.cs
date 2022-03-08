using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlastCollide : MonoBehaviour
{
    public Collider iLastEntered;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        iLastEntered = col;
    }

}
