using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopDmg : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Transform playerTransform;
    private Color textColor;
    private float dissapearTimer = 0.5f;
    private float fadeOutSpeed = 5f;
    private float moveYSpeed = 1f;
    
    public void setUp(int amount)
    {
        textMesh = GetComponent<TextMeshPro>();
        textColor = textMesh.color;
        textMesh.SetText(amount.ToString());
        playerTransform = Camera.main.transform;
    }
    private void LateUpdate() {
        transform.LookAt(2 * transform.position - playerTransform.position);

        transform.position += new Vector3(0f, moveYSpeed * Time.deltaTime, 0f);

        dissapearTimer -= Time.deltaTime;

        if (dissapearTimer <= 0f)
        {
            textColor.a -= fadeOutSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}

