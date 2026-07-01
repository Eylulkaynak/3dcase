using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Üretim Ayarları")]
    public GameObject toplanabilirPrefab; // Oluşturulacak eşya (Altın vs.)
    public Transform[] spawnNoktalari;    // Potansiyel doğma noktalarının listesi
    public int uretilecekMiktar = 5;      // Oyunda toplam kaç tane altın olsun?

    void Start()
    {
        // Oyun başladığında eşyaları dağıt
        EsyalariDagit();
    }

    void EsyalariDagit()
    {
        for (int i = 0; i < uretilecekMiktar; i++)
        {
            // Noktalar listesinden rastgele bir sıra numarası (index) seç
            int rastgeleIndex = Random.Range(0, spawnNoktalari.Length);
            
            // Seçilen noktayı hafızaya al
            Transform secilenNokta = spawnNoktalari[rastgeleIndex];

            // Eşyayı o noktada oluştur
            Instantiate(toplanabilirPrefab, secilenNokta.position, secilenNokta.rotation);
        }
    }
}