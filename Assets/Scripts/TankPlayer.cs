using UnityEngine;

public class TankPlayer : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 4f;
    public float rotateSpeed = 100f;

    [Header("射击设置")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireCD = 0.4f;
    public int maxBullet = 5;

    [Header("血量")]
    public int maxHP = 5;
    private int currentHP;

    public float invincibleTime = 0.5f;
    private float invincibleTimer;

    private float fireTimer;
    private int bulletCount;

    void Start()
    {
        currentHP = maxHP;
        GameManager.Instance.UpdateHP(currentHP);
    }

    void Update()
    {
        if (invincibleTimer > 0) invincibleTimer -= Time.deltaTime;
        if (fireTimer > 0) fireTimer -= Time.deltaTime;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, h * rotateSpeed * Time.deltaTime);

        LimitBorder();

        if (Input.GetKeyDown(KeyCode.Space) && fireTimer <= 0 && bulletCount < maxBullet)
        {
            Fire();
            fireTimer = fireCD;
        }
    }

    void Fire()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletCount++;
        AudioManager.Instance?.PlayFire();
    }

    public void BulletDestroy()
    {
        bulletCount--;
    }

    public void TakeDamage()
    {
        if (invincibleTimer > 0) return;

        currentHP--;
        invincibleTimer = invincibleTime;
        AudioManager.Instance?.PlayHurt();
        GameManager.Instance.UpdateHP(currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        AudioManager.Instance?.PlayGameOver();
        GameManager.Instance.SpawnExplosion(transform.position);
        GameManager.Instance.GameOver();
        Destroy(gameObject);
    }

    void LimitBorder()
    {
        float limit = 14f;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -limit, limit),
            transform.position.y,
            Mathf.Clamp(transform.position.z, -limit, limit)
        );
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            transform.Translate(-transform.forward * 0.1f);
        }
    }
}
