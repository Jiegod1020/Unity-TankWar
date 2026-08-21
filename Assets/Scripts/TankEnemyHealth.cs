using UnityEngine;

public class TankEnemyHealth : MonoBehaviour
{
    public int hp = 2;
    public int score = 10;

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.Instance.AddScore(score);
        GameManager.Instance.EnemyDie();
        GameManager.Instance.SpawnExplosion(transform.position);
        AudioManager.Instance?.PlayExplode();
        Destroy(gameObject);
    }
}
