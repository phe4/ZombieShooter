using UnityEngine;
using EZCameraShake;

public class AnimEventLarge : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Transform boss;
    private Vector3 p ;
    private Vector3 b ;
    void EndAttacking()
    {
        GetComponent<BossController>().state = "run";
        AudioSource shake = GetComponent<AudioSource>();
        CameraShaker.Instance.ShakeOnce(10f, 4f, .1f, 2f);
        if (!shake.isPlaying)
        {
            shake.Play();
        }
        
        PlayerDamageable damageable = player.gameObject.GetComponent<PlayerDamageable>();
        float dist = Vector3.Distance(b, p);
        if (damageable && dist < 5f)
        {
            damageable.InflictDamage(50.0f);
            DI_System.CreateIndicator(this.transform);
        }
    }

    private void Awake()
    {
        player = GameObject.Find("FPCharacterControlller_copy").transform;
    }
    private void Update() {
        p = new Vector3(player.position.x, 0f, player.position.z);
        b = new Vector3(gameObject.transform.position.x, 0f, gameObject.transform.position.z);
    }
}
