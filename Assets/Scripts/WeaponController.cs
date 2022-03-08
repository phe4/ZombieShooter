using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{

    public GameObject WeaponRoot;
    //是否有武器
    public bool isWeaponActive { get; private set; }
    //拥有枪的人
    public GameObject Owner { get; set; }
    //武器实际3d模型
    public GameObject SourcePrefab { get; set; }
    //枪口火焰的位置
    public Transform weaponMuzzle;
    //枪口火焰prefab
    public GameObject muzzleFlashPrefab;
    //
    public Vector3 muzzleWorldVelocity{ get; private set; }

    private AudioSource AK47shoot;

    private AudioSource AK47Reload;

    private AudioSource AK47Continue;

    private AudioSource AK47End;
    public AudioClip[] a1;
    //两次射击间隔
    public float delayBetweenShots = 0.1f;
    //上一次射击时间
    private float lastShotTime = Mathf.NegativeInfinity;
    //
    public ProjectileBase projectilePrefab;
    private Inventory inventory;

    public int AmmoInMag = 30;
    public static int maxAmmoCarried = 210;

    public string GetCurrentAmmo => isReloaded ? CurrentAmmo.ToString() : "Reloading";
    public string GetCurrentAmmoCarried => CurrentAmmoCarried.ToString();

    protected int CurrentAmmo;
    protected static int CurrentAmmoCarried;

    private bool isReloaded = true;
    //显示武器
    private Camera weaponCamera;
    public void ShowWeapon(bool show)
    {
        WeaponRoot.SetActive(show);
        isWeaponActive = show;
    }

    // Start is called before the first frame update
    void Start()
    {
        weaponCamera = GameObject.Find("WeaponCamera").GetComponent<Camera>();
        CurrentAmmo = AmmoInMag;
        CurrentAmmoCarried = 60;
        inventory = GetComponentInParent<PickUp>().inventory;
        this.gameObject.AddComponent<AudioSource>();
        this.gameObject.AddComponent<AudioSource>();

        var as_array = this.gameObject.GetComponents(typeof(AudioSource));
        AK47shoot = (AudioSource)as_array[0];
        AK47Reload = (AudioSource)as_array[1];
        AK47shoot.clip = a1[0];
        AK47Reload.clip = a1[1];
        AK47shoot.volume = 0.1f;
        AK47Reload.volume = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    //检测是否按下射击按钮
    public bool HandleShootInputs(bool inputHeld)
    {
        if (inputHeld && isReloaded)
        {
            return TryShoot();
        }
        return false;
    }

    public bool HandleReloadInputs(bool inputHeld)
    {
        if (inputHeld)
        {
            return TryReload();
        }
        return false;
    }
    //检测能否射击
    private bool TryShoot()
    {
        if (CurrentAmmo <= 0)
        {
            TryReload();
            return false;
        }
        if(lastShotTime + delayBetweenShots < Time.time)
        {
            CurrentAmmo -= 1;
            HandleShoot();
            return true;
        }
        return false;
    }

    //射击
    private void HandleShoot()
    {
        //射出弹道

        AK47shoot.Play();

        if (projectilePrefab != null)
        {
            //方向位置和火焰方向一致
            Vector3 shotDirection = weaponMuzzle.forward;
            ProjectileBase newProjectile = Instantiate(projectilePrefab, weaponCamera.transform.position, weaponCamera.transform.rotation, weaponMuzzle.transform);
            //ProjectileBase newProjectile = Instantiate(projectilePrefab, weaponCamera.transform.position, weaponCamera.transform.rotation, weaponCamera.transform);
            newProjectile.Shoot(controller:this);
        }

        //射击火焰特效
        if(muzzleFlashPrefab != null)
        {
            //创建实例
            GameObject muzzleFlashInstance = Instantiate(muzzleFlashPrefab,weaponMuzzle.position,weaponMuzzle.rotation,weaponMuzzle.transform);
            //消除实例
            Destroy(muzzleFlashInstance, 2);
        }
        lastShotTime = Time.time;
    }

    private bool TryReload()
    {
        if (CurrentAmmo == AmmoInMag || CurrentAmmoCarried == 0) return false;
        var AmmoCount = AmmoInMag - CurrentAmmo;
        isReloaded = false;
        if (!AK47Reload.isPlaying)
        {
            AK47Reload.Play();
        }
        Invoke("Reload", 2.0f);
        //Reload();
        return true;
    }

    private void Reload()
    {
        var AmmoCount = AmmoInMag - CurrentAmmo;


        if (AmmoCount > CurrentAmmoCarried)
        {
            CurrentAmmo = CurrentAmmo + CurrentAmmoCarried;
            inventory.AddItem(new Item { itemType = 0, amount = -CurrentAmmoCarried });
            CurrentAmmoCarried = 0;
        }
        else
        {
            CurrentAmmo = AmmoInMag;
            CurrentAmmoCarried -= AmmoCount;
            inventory.AddItem(new Item { itemType = 0, amount = -AmmoCount });
        }
        isReloaded = true;
    }

    public void AddAmmo(int count)
    {
        CurrentAmmoCarried += count;

        if(CurrentAmmoCarried >= maxAmmoCarried)
        {
            CurrentAmmoCarried = maxAmmoCarried;
        }
    }
}
