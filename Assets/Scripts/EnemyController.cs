using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer, whatIsObst;

    public float health;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public AudioClip[] a1;
    public float sightRange, attackRange;
    [Range(0,360)]
    public float angle;
    
    public Vector3 distance;
    public bool inSight;
    private Animation anime;
    public bool isDead = false;
    //public bool isSet = false;
    public bool isChase = false;
    public string state = "idle";

    private bool playerInSightRange, playerInAttackRange;
    private AudioSource zombieShout;
    private AudioSource zombieDead;
    private Rigidbody _rigidbody;
    [SerializeField]private Transform damagePopupTransform;

    private void Awake()
    {
        player = GameObject.Find("FPCharacterControlller_copy").transform;
        agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        agent.speed = 6.0f;
        anime = GetComponent<Animation>();
        anime["Death"].wrapMode = WrapMode.Once;
    }
    private void Start()
    {
        this.gameObject.AddComponent<AudioSource>();
        this.gameObject.AddComponent<AudioSource>();
        var as_array = this.gameObject.GetComponents(typeof(AudioSource));
        zombieShout = (AudioSource)as_array[0];
        zombieDead = (AudioSource)as_array[1];
        zombieShout.clip = a1[Random.Range(1, 3)];
        zombieDead.clip = a1[0];
        zombieShout.volume = 0.3f;
        zombieDead.volume = 0.1f;
    }
    private void Update()
    {
        //check if player is in sight
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        distance = Vector3.Normalize(transform.position - player.position)*1.8f;
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        
        if (playerInSightRange){
            if (Vector3.Angle(transform.forward, directionToPlayer) < angle /2) 
            {
                float dist = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, directionToPlayer, dist, whatIsObst))
                {
                    inSight = true;
                }
                else{
                    inSight = false;
                }   
            }
            else{
                inSight = false;
            }
        }
        else if (inSight)
        {
            inSight = false;
        }
        

        if (!inSight && !playerInAttackRange && !isDead && !isChase)
        {
            //isSet = false;
            Idle();
        }
        if ((inSight || isChase) && !playerInAttackRange && !isDead && state !="attacking")
        {
            //if (!isSet)
            //{
            //    enemySpeed = 2.0f;
            //    isSet = true;
            //}
            isChase = true;
            ChasePlayer();
        }
        if ((inSight && playerInAttackRange && !isDead))
        {
            //isSet = false;
            AttackPlayer();
        }

    }


    private void Idle()
    {
        state = "idle";
        anime.Play("Idle");
        agent.isStopped = true;
    }

    private void ChasePlayer()
    {
        agent.isStopped = false;
        anime.Play("Run");
        state = "run";
        //enemySpeed += 0.02f;
        //if (enemySpeed >= 5.0f)
        //{
        //    enemySpeed = 5.0f;
        //}
        //agent.velocity = (player.position - transform.position).normalized * enemySpeed;
        //agent.destination = player.position;
        agent.speed = 8.0f;
        agent.acceleration = 10.0f;
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        if (!zombieShout.isPlaying)
        {
            zombieShout.Play();
        }
        anime.Play("Attack1");
        state = "attacking";
        // agent.SetDestination(player.position + distance);

        transform.LookAt(player);
        // DI_System.CreateIndicator(this.transform);
        if (!alreadyAttacked)
        {
            //TODO:attack code here

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        int dmg = (int)damage;
        PopDmgManager.instance.DisplayDmg(dmg, damagePopupTransform);

        if(!isDead && health <= 0)
        {
            isDead = true;
            zombieDead.Play();
            anime.Play("Death");
            state = "dead";
            agent.isStopped = true;
            Invoke(nameof(DestroyEnemy), 1.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}

