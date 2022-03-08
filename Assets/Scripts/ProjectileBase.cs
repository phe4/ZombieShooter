using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//保存projectile的各种属性
public class ProjectileBase : MonoBehaviour
{
    //射击者
    public GameObject Owner { get; private set; }
    //初始位置
    public Vector3 initialPositon { get; private set; }
    //初始方向
    public Vector3 initialDirection { get; private set; }
    //
    public Vector3 inheritedMuzzleVelocity { get; private set; }

    public UnityAction onShoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(WeaponController controller)
    {
        Owner = controller.Owner;
        initialPositon = transform.position;
        initialDirection = transform.forward;

        inheritedMuzzleVelocity = controller.muzzleWorldVelocity;

        if(onShoot != null)
        {
            onShoot.Invoke();
        }
    }
}
