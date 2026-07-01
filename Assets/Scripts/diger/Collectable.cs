using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Toplama Ayarları")]
    public int deger = 10; // Objenin vereceği puan (score) miktarı

    // Başka bir obje bu objenin içinden (Trigger) geçtiğinde çalışır
    private void OnTriggerEnter(Collider other)
    {
        // İçinden geçen objenin etiketi "Player" mı?
        if (other.CompareTag("Player"))
        {
            // GameManager sahnede var mı diye kontrol edip işlemleri ona yaptırıyoruz
            if (GameManager.Instance != null)
            {
                // Sol üstteki Items: X / Y sayısını artırır
                GameManager.Instance.CollectItem(); 
                
                // Score: X kısmını artırır
                GameManager.Instance.AddScore(deger); 
            }

            Debug.Log("Tebrikler, obje toplandı! Kazanılan skor: " + deger);

            // Obje toplandığı için sahneden silinmeli
            Destroy(gameObject);
        }
    }
}