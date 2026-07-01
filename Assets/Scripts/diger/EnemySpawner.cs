using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Ayarları")]
    [Tooltip("Oluşturulacak düşman prefabını buraya sürükle")]
    public GameObject enemyPrefab; 
    
    [Tooltip("Düşmanların doğacağı noktalar")]
    public Transform[] spawnPoints;

    [Tooltip("İki düşmanın doğması arasındaki bekleme süresi (Saniye)")]
    public float spawnDelay = 5f; // Süreyi Unity içinden değiştirebilmen için değişkene bağladık

    private void Start()
    {
        // Zamanlayıcılı sistemi (Coroutine) başlatıyoruz
        StartCoroutine(SpawnEnemiesSequentially());
    }

    // IEnumerator: Zaman içinde duraklayarak çalışabilen özel bir fonksiyondur
    private IEnumerator SpawnEnemiesSequentially()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Spawner'da düşman prefabı eksik!");
            yield break; // Hata varsa işlemi durdur
        }

        // Listedeki her bir nokta için sırayla işlemi yap
        foreach (Transform point in spawnPoints)
        {
            // 1. Düşmanı oluştur
            Instantiate(enemyPrefab, point.position, point.rotation);

            // 2. Kodun geri kalanını çalıştırmadan önce 'spawnDelay' (5) saniye bekle
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}