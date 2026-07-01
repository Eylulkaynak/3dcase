using UnityEngine;

public class LootPickUp : MonoBehaviour
{
    [Header("Ganimet Ayarları")]
    public int itemValue = 1; // Kaç altın/puan verecek?

    // Oyuncu topun "Trigger" alanına girdiğinde otomatik çalışır
    private void OnTriggerEnter(Collider other)
    {
        // Temas eden obje "Player" etiketine (Tag) sahip mi?
        if (other.CompareTag("Player"))
        {
            CollectLoot();
        }
    }

    private void CollectLoot()
    {
        Debug.Log("Ganimet alındı! Kazanılan değer: " + itemValue);

        // Burada ileride oyuncunun altınını veya envanterini artıran bir kod çağırabilirsin.
        // Örnek: other.GetComponent<PlayerInventory>().AddCoin(itemValue);

        // Alındığı an topu sahneden sil
        Destroy(gameObject);
    }
}