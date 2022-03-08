using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStandard : MonoBehaviour
{
    //在游戏场景存活时间
    public float maxLifeTime = 5f;
    //速度
    public float speed = 300f;

    public Transform root;
    //子弹尖端的位置
    public Transform tip;
    //用于碰撞检测球体半径
    public float collider_radius = 0.01f;

    public LayerMask hittableLayers = -1;

    public GameObject []impactVFX;

    public float impactVFXLifeTime = 5f;
    //击打到墙壁后粒子的偏移量
    public float impactVFXSpawnOffset = 0.1f;

    public float damage;

    public AudioSource headShotAudio;

    private GameObject FPcharacterController;

    private ProjectileBase _projectileBase;

    private Vector3 _velocity;
    //每一帧移动的长度
    public float trajectoryCorrectionDistance = 5f;
    //相对最终的向量已经移动的向量的大小
    private Vector3 _consumedCorrectionVector;
    //校准准心
    private bool _hasTrajectoryCorrected;
    //弹道校准向量
    private Vector3 _correctionVector;

    //跟踪上次射击起始位置
    private Vector3 _lastRootPostion;

    private void OnEnable()
    {
        _projectileBase = GetComponent<ProjectileBase>();
        _projectileBase.onShoot += OnShoot;
        Destroy(gameObject, maxLifeTime);
    }
    //正在射击的时候设置速度
    private void OnShoot()
    {
        _lastRootPostion = root.position;
        _velocity += transform.forward * speed;

        WeaponManager weaponManger = _projectileBase.Owner.GetComponent<WeaponManager>();

        if (weaponManger)
        {
            _hasTrajectoryCorrected = false;
            Transform weaponCameraTransform = weaponManger.weaponCamera.transform;
            
            Vector3 cameraToMuzzle = _projectileBase.initialPositon - weaponCameraTransform.position;
            //Debug.DrawRay(weaponCameraTransform.position, cameraToMuzzle, Color.yellow, 6);

            //将任意方向向量投影到指定平面 通过法线
            _correctionVector = Vector3.ProjectOnPlane(-cameraToMuzzle,weaponCameraTransform.forward);
            //Debug.DrawRay(weaponCameraTransform.position, -cameraToMuzzle, Color.red, 6);
            //Debug.DrawRay(weaponCameraTransform.position, weaponCameraTransform.forward, Color.green, 6);
            //Debug.DrawRay(weaponCameraTransform.position, _correctionVector, Color.magenta, 6);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FPcharacterController = GameObject.Find("FPCharacterControlller_copy");
    }

    // Update is called once per frame
    void Update()
    {
        //移动
        transform.position += _velocity * Time.deltaTime;
        //方向
        transform.forward = _velocity.normalized;

        //子弹弹道运行轨迹移项屏幕中心
        if (!_hasTrajectoryCorrected && _consumedCorrectionVector.sqrMagnitude < _correctionVector.sqrMagnitude)
        {
            //有多少向量需要更新
            Vector3 correctionLeft = _correctionVector - _consumedCorrectionVector;
            float distanceThisFrame = (root.position - _lastRootPostion).magnitude;
            Vector3 correctionThisFrame = (distanceThisFrame / trajectoryCorrectionDistance) * _correctionVector;
            correctionThisFrame = Vector3.ClampMagnitude(correctionThisFrame, correctionLeft.magnitude);

            _consumedCorrectionVector += correctionThisFrame;

            if (Mathf.Abs(_consumedCorrectionVector.sqrMagnitude - _correctionVector.sqrMagnitude) < Mathf.Epsilon)
            {
                _hasTrajectoryCorrected = true;
            }

            //开始调整弹道
            transform.position += correctionThisFrame;
        }

        //碰撞检测
        RaycastHit closestHit = new RaycastHit();
        closestHit.distance = Mathf.Infinity;

        bool foundHit = false;

        Vector3 displacementSinceLastFrame = tip.position - _lastRootPostion;
        //SphereCastAll 和 sphereCast 的区别 是返回值是个数组 所有和这个射线交互的对象都返回到这个数组里
        RaycastHit[] hits = Physics.SphereCastAll(_lastRootPostion,
            collider_radius,
            displacementSinceLastFrame.normalized,
            displacementSinceLastFrame.magnitude,
            hittableLayers,
            QueryTriggerInteraction.Collide
        );

        foreach (RaycastHit hit in hits)
        {
            //有效撞击
            if (isHitValid(hit) && hit.distance < closestHit.distance)
            {
                closestHit = hit;
                foundHit = true;
            }
        }

        if (foundHit)
        {
             //无效射击处理
             if(closestHit.distance <= 0)
            {
                //无效果
                closestHit.point = root.position;
                closestHit.normal = -transform.forward;
            }

            OnHit(closestHit.point, closestHit.normal, closestHit.collider);
        }
    }

    private bool isHitValid(RaycastHit hit)
    {
        if (hit.collider.isTrigger)
        {
            return false;
        }
        return true;
    }
    //用来处理碰撞后的音效和特效
    private void OnHit(Vector3 point, Vector3 normal,Collider collider)
    {
        Damageable damageable = collider.GetComponent<Damageable>();
        BossDamageable bossdamageable = collider.GetComponent<BossDamageable>();
        if (damageable)
        {
            // Rigidbody rb = collider.GetComponent<Rigidbody>();
            // Vector3 direction = normal;
            // direction.y = 0;
            // rb.AddForce(direction.normalized * 1, ForceMode.Impulse);
            EnemyController damageableAncestor = damageable.getAncestor(damageable.gameObject).GetComponent<EnemyController>();
            if (damageable.gameObject.name == "Character1_Head")
            {
                damageable.InflictDamage(60f);
                if(damageableAncestor.health <= 0)
                {
                    FPcharacterController.GetComponent<AudioManager>().PlayHeadShotAudio();
                }
            }
            else
            {
                damageable.InflictDamage(damage);
            }
        }
        
        if (bossdamageable)
        {
            // Rigidbody rb = collider.GetComponent<Rigidbody>();
            // Vector3 direction = normal;
            // direction.y = 0;
            // rb.AddForce(direction.normalized * 1, ForceMode.Impulse);
            //BossController bossdamageableAncestor = bossdamageable.getAncestor(bossdamageable.gameObject).GetComponent<BossController>();
            if (bossdamageable.gameObject.name == "Eyes")
            {
                bossdamageable.InflictDamage(100f);
            }
            else
            {
                bossdamageable.InflictDamage(damage);
            }
        }

        if(impactVFX != null)
        {
            GameObject impactVFXInstance;
            if(bossdamageable)
            {
                impactVFX[1].transform.localScale = new Vector3(1, 4, 10);
                impactVFXInstance = Instantiate(impactVFX[1],
                point,
                Quaternion.LookRotation(normal));
            }
            else if (damageable)
            {
                impactVFXInstance = Instantiate(impactVFX[1],
                point,
                Quaternion.LookRotation(normal));
            }
            else
            {
                 impactVFXInstance = Instantiate(impactVFX[0],
                                point + normal * impactVFXSpawnOffset,
                                Quaternion.LookRotation(normal));

            }
            if(impactVFXLifeTime > 0)
            {
                Destroy(impactVFXInstance, impactVFXLifeTime);
            }
        }
        Destroy(gameObject);
    }
}