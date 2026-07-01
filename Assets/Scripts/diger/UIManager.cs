using UnityEngine;
using TMPro; // TextMeshPro kütüphanesini dahil ediyoruz

public class UIManager : MonoBehaviour
{
    // Singleton yapısı: Diğer scriptlerin bu scripte kolayca ulaşmasını sağlar
    public static UIManager instance;

    [Header("UI Metinleri (Text References)")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI itemsText;
    public TextMeshProUGUI killsText;

    // Arka planda tutulan veriler
    private int score = 0;
    private int collectedItems = 0;
    private int killCount = 0;

    private void Awake()
    {
        // Singleton Kurulumu
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Oyun başladığında yazıları varsayılan değerleriyle güncelle
        UpdateUI();
        
        // Örnek başlangıç canı (Bunu PlayerHealth scriptin olduğunda oradan çağıracağız)
        UpdatePlayerHealth(100); 
    }

    // Skor ekleme fonksiyonu
    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    // Eşya toplama fonksiyonu
    public void AddCollectedItem(int amount)
    {
        collectedItems += amount;
        UpdateUI();
    }

    // Düşman öldürme fonksiyonu
    public void AddKill()
    {
        killCount++;
        UpdateUI();
    }

    // Oyuncu canını güncelleme fonksiyonu
    public void UpdatePlayerHealth(int currentHealth)
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }

    // Ekrandaki yazıları yenileyen ana fonksiyon
    private void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + score.ToString();
        if (itemsText != null) itemsText.text = "Items: " + collectedItems.ToString();
        if (killsText != null) killsText.text = "Kills: " + killCount.ToString();
    }
}