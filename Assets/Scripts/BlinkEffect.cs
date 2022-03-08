using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlinkEffect : MonoBehaviour
{
    public float glowMaxSize;
    public float timeDif = 0.1f;
    private float lasttime = 0;
    private bool isTrue = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //SerializedObject halo = new SerializedObject(GetComponent("Halo"));
        //halo.FindProperty("m_Size").floatValue = Mathf.PingPong(Time.time, glowMaxSize);
        //halo.ApplyModifiedProperties();
        Component halo = GetComponent("Halo");
        if (Time.time - lasttime >= timeDif)
        {
            lasttime = Time.time;
            halo.GetType().GetProperty("enabled").SetValue(halo, !isTrue);
            isTrue = !isTrue;
        }
    }
}
