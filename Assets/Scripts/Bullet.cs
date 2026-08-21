using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 12f;
    public float lifeTime = 2f;
    public int damage = 1;

    private string shooterTag;

    void Start()
    {
        Destroy(gameObject, lifeTime);
        shooterTag = transform.parent != null ? transform.parent.tag : "";
    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) return;

        if (other.CompareTag("Wall"))
        {
            DestroyBullet();
        }

        if (shooterTag == "Player" && other.CompareTag("Enemy"))
        {
            EnemyHurt(other.gameObject);
            DestroyBullet();
        }

        if (shooterTag == "Enemy" && other.CompareTag("Player"))
        {
            TankPlayer pl = other.GetComponent<TankPlayer>();
            if(pl != null) pl.TakeDamage();
            DestroyBullet();
        }
    }

    void EnemyHurt(GameObject enemy)
    {
        TankEnemyHealth hp = enemy.GetComponent<TankEnemyHealth>();
        if (hp != null)
        {
            hp.TakeDamage(damage);
        }
    }

    void DestroyBullet()
    {
        if (shooterTag == "Player")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.GetComponent<TankPlayer>().BulletDestroy();
            }
        }
        Destroy(gameObject);
    }
}
