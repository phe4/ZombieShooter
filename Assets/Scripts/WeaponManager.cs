using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    //
    public List<WeaponController> startingWeapons = new List<WeaponController>();

    public Text AmmoCountText;

    public WeaponController activeWeapon;

    public Camera weaponCamera;
    //武器显示位置
    public Transform WeaponParentSocket;
    //回调函数
    public UnityAction<WeaponController> onSwitchedToWeapon;
    //武器库
    private WeaponController[] _weaponSlots = new WeaponController[9];

    // Start is called before the first frame update
    private void Start()
    {
        onSwitchedToWeapon += OnWeaponSwitched;

        foreach(WeaponController weapon in startingWeapons)
        {
            AddWeapon(weapon);
        }

        Switchweapon();
    }

    // Update is called once per frame
    void Update()
    {
        //获取武器库的第一个武器
        activeWeapon = _weaponSlots[0];
        if (activeWeapon)
        {
            //按下射击按键
            activeWeapon.HandleShootInputs(PlayerInputHandler.Instance.GetFireInputHeld());
            activeWeapon.HandleReloadInputs(PlayerInputHandler.Instance.GetReloadInputHeld());
            UpdateAmmoInfo(activeWeapon.GetCurrentAmmo, activeWeapon.GetCurrentAmmoCarried);
        }
    }

    //将武器存储到
    public bool AddWeapon(WeaponController weaponPrefab)
    {
        for(int i = 0; i < _weaponSlots.Length; i++)
        {
            if(_weaponSlots[i] == null)
            {
                WeaponController weaponInstance = Instantiate(weaponPrefab, WeaponParentSocket);
                //起始位置localposition
                weaponInstance.transform.localPosition = Vector3.zero;
                //初始化为无旋转
                weaponInstance.transform.localRotation = Quaternion.identity;

                weaponInstance.Owner = gameObject;
                weaponInstance.SourcePrefab = weaponPrefab.gameObject;
                //初始化为不显示
                weaponInstance.ShowWeapon(false);
                _weaponSlots[i] = weaponInstance;

                return true;
            }
            
        }
        return false;
    }
    //切换武器
    public void Switchweapon()
    {
        
        SwitchWeaponToIndex(0);
    }

    public void SwitchWeaponToIndex(int newWeaponIndex)
    {
        if(newWeaponIndex >= 0)
        {
            // 获得武器库中第Index个武器
            WeaponController newWeapon = GetWeaponAtSlotIndex(newWeaponIndex);

            if(onSwitchedToWeapon != null)
            {
                //显示这个武器 会调用OnWeaponSwitched
                onSwitchedToWeapon.Invoke(newWeapon);
            }
        }
    }

    public WeaponController GetWeaponAtSlotIndex(int index)
    {
        if(index >=0 && index < _weaponSlots.Length)
        {
            return _weaponSlots[index];
        }
        return null;
    }

    private void OnWeaponSwitched(WeaponController newWeapon)
    {
        if (newWeapon != null)
        {
            newWeapon.ShowWeapon(true);
        }
    }

    private void UpdateAmmoInfo(string _ammo, string _ammoCarried)
    {
        if(_ammo == "Reloading")
        {
            AmmoCountText.text = "Ammo: " + _ammo;
        }
        else
        {
            AmmoCountText.text = "Ammo: " + _ammo + "/" + _ammoCarried;
        }
    }
}
