using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("波次设置")]
    public int startEnemyCount = 2;
    public int waveAddEnemy = 1;
    public float spawnInterval = 3f;
    public Transform[] spawnPoints;

    [Header("UI")]
    public Text scoreText;
    public Text waveText;
    public Text hpText;
    public GameObject gameOverPanel;
    public Button restartBtn;

    [Header("资源")]
    public GameObject enemyPrefab;
    public GameObject explosionEffect;

    public int score;
    public int currentWave;
    public int enemyAliveCount;
    public bool isGameOver;

    private float spawnTimer;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        InitGame();
        restartBtn.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        if (isGameOver) return;

        spawnTimer -= Time.deltaTime;
        int maxEnemyThisWave = startEnemyCount + (currentWave - 1) * waveAddEnemy;
        if (spawnTimer <= 0 && enemyAliveCount < maxEnemyThisWave)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }

        if (enemyAliveCount <= 0)
        {
            NextWave();
        }
    }

    void InitGame()
    {
        score = 0;
        currentWave = 1;
        isGameOver = false;
        enemyAliveCount = 0;
        gameOverPanel.SetActive(false);
        UpdateUI();
    }

    void SpawnEnemy()
    {
        int randomPos = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[randomPos].position, spawnPoints[randomPos].rotation);
        enemyAliveCount++;
        UpdateUI();
    }

    void NextWave()
    {
        currentWave++;
        spawnTimer = 1f;
    }

    public void AddScore(int num)
    {
        score += num;
        UpdateUI();
    }

    public void EnemyDie()
    {
        enemyAliveCount--;
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void UpdateUI()
    {
        scoreText.text = $"分数：{score}";
        waveText.text = $"波次：{currentWave}";
    }

    public void UpdateHP(int hp)
    {
        hpText.text = $"血量：{hp}";
    }

    public void SpawnExplosion(Vector3 pos)
    {
        GameObject effect = Instantiate(explosionEffect, pos, Quaternion.identity);
        Destroy(effect, 0.8f);
    }
}
