using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DamageIndicator : MonoBehaviour
{
    private const float MaxTimer = 2.0f;
    private float timer = MaxTimer;
    private CanvasGroup canvasGroup;
    protected CanvasGroup CanvasGroup
    {
        get
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }
            return canvasGroup;
        }
    }
    private RectTransform rect;
    protected RectTransform Rect
    {
        get
        {
            if (rect == null)
            {
                rect = GetComponent<RectTransform>();
                if (rect == null)
                {
                    rect = gameObject.AddComponent<RectTransform>();
                }
            }
            return rect;
        }
    }

    public Transform target {get; protected set;} = null;
    private Transform player;
    private IEnumerator counter = null;
    private Action unRegister = null;
    private Quaternion tRot = Quaternion.identity;
    private Vector3 tPos = Vector3.zero;

    public void Register(Transform t, Transform p, Action unReg)
    {
        this.target = t;
        this.player = p;
        this.unRegister = unReg;

        StartCoroutine(RotateToTheTarget());
        StartTimer();
    }
    public void Restart()
    {
        timer = MaxTimer;
        StartTimer();
    }
    private void StartTimer()
    {
        if (counter != null)
        {
            StopCoroutine(counter);
        }
        counter = CountDown();
        StartCoroutine(counter);
    }
    private IEnumerator CountDown()
    {
        while (CanvasGroup.alpha < 1.0f)
        {
            CanvasGroup.alpha += 4 * Time.deltaTime;
            yield return null;
        }
        while (timer > 0)
        {
            timer--;
            yield return new WaitForSeconds(1);
        }
        while (CanvasGroup.alpha > 0.0f)
        {
            CanvasGroup.alpha -= 2 * Time.deltaTime;
            yield return null;
        }
        unRegister();
        Destroy(gameObject);
    }
    IEnumerator RotateToTheTarget()
    {
        while (enabled)
        {
            if(target)
            {
                tPos = target.position;
                tRot = target.rotation;
            }
            Vector3 direction = player.position - tPos;
            tRot = Quaternion.LookRotation(direction);
            tRot.z = -tRot.y;
            tRot.x = 0;
            tRot.y = 0;

            Vector3 northDirection = new Vector3(0, 0, player.eulerAngles.y);
            Rect.localRotation = tRot * Quaternion.Euler(northDirection);

            yield return null;
        }
    }
}
