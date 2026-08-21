using UnityEngine;

public class TankEnemyAI : MonoBehaviour
{
    [Header("AI参数")]
    public float moveSpeed = 1.5f;
    public float fireCD = 1.8f;
    public float detectDistance = 15f;
    public float stopDistance = 5f;

    public Transform firePoint;
    public GameObject bulletPrefab;

    private float fireTimer;
    private Transform player;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if(p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null || GameManager.Instance.isGameOver) return;

        float distance = Vector3.Distance(transform.position, player.position);

        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 2f * Time.deltaTime);

        if (distance > stopDistance)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0 && distance < detectDistance)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            fireTimer = fireCD;
        }

        LimitBorder();
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
            transform.Translate(-transform.forward * 0.05f);
        }
    }
}
