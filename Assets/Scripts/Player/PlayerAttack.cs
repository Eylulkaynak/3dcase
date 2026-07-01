using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject projectilePrefab; // Mermi prefabını buraya sürükle
    public Transform firePoint;         // Merminin çıkacağı boş objeyi buraya sürükle
    public float fireForce = 20f;       // Merminin gidiş hızı

    void Update()
    {
        // Farenin sol tıkına basıldığında ateş et
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Mermiyi firePoint pozisyonunda oluştur
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Merminin Rigidbody bileşenini al
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        
        // Mermiye ileriye doğru fiziksel bir güç (Impulse) uygula
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
        }

        // Mermi hiçbir şeye çarpmazsa uzaya gidip RAM'i doldurmasın diye 3 saniye sonra sil
        Destroy(newProjectile, 3f);
    }
}