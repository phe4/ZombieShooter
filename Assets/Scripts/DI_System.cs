using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DI_System : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DamageIndicator indicatorPrefab = null;
    [SerializeField] private RectTransform holder = null;
    [SerializeField] private new Camera camera = null;
    [SerializeField] private Transform player = null;
    private Dictionary<Transform, DamageIndicator> indicators = new Dictionary<Transform, DamageIndicator>();

    #region Delegates
    public static Action<Transform> CreateIndicator = delegate {};
    public static Func<Transform, bool> CheckIfObjectInSight = null;
    #endregion

    private void OnEnable() 
    {
        CreateIndicator += Create;
        CheckIfObjectInSight += inSight;
    }
    private void OnDisable() 
    {
        CreateIndicator -= Create;
        CheckIfObjectInSight += inSight;
    }
    void Create(Transform target)
    {
        if (indicators.ContainsKey(target))
        {
            indicators[target].Restart();
            return;
        }
        DamageIndicator newIndicator = Instantiate(indicatorPrefab, holder);
        newIndicator.Register(target, player, new Action(() => { indicators.Remove(target); }));

        indicators.Add(target, newIndicator);
    }
    bool inSight(Transform t)
    {
        Vector3 screenPoint = camera.WorldToViewportPoint(t.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}
