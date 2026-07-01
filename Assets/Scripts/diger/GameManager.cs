using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Stats")]
    public int score;
    public int enemyKillCount;
    public int totalEnemies = 3;
    public int collectedItems;
    public int totalCollectibles = 5;

    [Header("Game State")]
    public bool isGameEnded;

    [Header("UI Texts")]
    public TMP_Text scoreText;
    public TMP_Text healthText;
    public TMP_Text killText;
    public TMP_Text collectText;
    public TMP_Text messageText;

    // PlayerHealth referansını performansı artırmak için hafızada tutuyoruz
    private PlayerHealth playerHealth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
        isGameEnded = false;

        // PlayerHealth scriptini sahneyi yormamak için sadece başlangıçta bir kez buluyoruz
        playerHealth = FindObjectOfType<PlayerHealth>();

        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (healthText != null)
            healthText.text = "Health: " + GetPlayerHealthText();

        if (killText != null)
            killText.text = "Kills: " + enemyKillCount + " / " + totalEnemies;

        if (collectText != null)
            collectText.text = "Items: " + collectedItems + " / " + totalCollectibles;
    }

    private string GetPlayerHealthText()
    {
        // Eğer sahnede PlayerHealth bulunamadıysa (veya oyuncu öldüyse) hata vermemesi için koruma
        if (playerHealth == null)
            return "0";

        return playerHealth.GetCurrentHealth().ToString();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void EnemyKilled()
    {
        enemyKillCount++;
        AddScore(10); // AddScore zaten UpdateUI'ı çağırdığı için burada tekrar çağırmamıza gerek kalmadı
        
        /* if (enemyKillCount >= totalEnemies)
        {
            WinGame();
        }
        */
    }

    public void CollectItem()
    {
        collectedItems++;
        UpdateUI();
    }

    public void WinGameFromDoor()
    {
        WinGame();
    }

    public void WinGame()
    {
        if (isGameEnded) return;
        isGameEnded = true;

        if (messageText != null)
        {
            messageText.text = "You Win!";
            messageText.gameObject.SetActive(true);
        }

        EndGameState();
    }

    public void GameOver()
    {
        if (isGameEnded) return;
        isGameEnded = true;

        if (messageText != null)
        {
            messageText.text = "Game Over!";
            messageText.gameObject.SetActive(true);
        }

        EndGameState();
    }

    // Oyun bittiğinde yapılacak ortak işlemleri tek bir yere topladık
    private void EndGameState()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}