using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 40f;
    public float lifeTime = 3f;
    public int damage = 25;

    [Tooltip("Merminin çarpabileceği katmanlar (Player'ı dışarıda bırakmak için önemli)")]
    public LayerMask hitLayers = ~0; 

    private Vector3 moveDirection;

    private void OnEnable()
    {
        // Havuz (Pooling) sistemi için mermi her aktif olduğunda süreyi sıfırla
        CancelInvoke(nameof(DeactivateBullet));
        Invoke(nameof(DeactivateBullet), lifeTime);
    }

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;
        
        // Merminin ucunu gidiş yönüne çevir
        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }
    }

    private void Update()
    {
        float moveStep = speed * Time.deltaTime;

        // Duvardan veya düşmanın içinden geçmeyi engellemek için Raycast kontrolü
        if (Physics.Raycast(transform.position, moveDirection, out RaycastHit hit, moveStep, hitLayers))
        {
            HitTarget(hit.collider.gameObject);
            return; 
        }

        // Çarpma yoksa ilerle
        transform.position += moveDirection * moveStep;
    }

    // Yedek Kontrol: Eğer mermi hedefin tam içinde doğarsa
    private void OnTriggerEnter(Collider other)
    {
        // Çarptığımız obje hitLayers içindeyse hedefe vur
        if ((hitLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            HitTarget(other.gameObject);
        }
    }

    private void HitTarget(GameObject target)
    {
        // Çarptığımız objede IDamageable var mı bak, varsa hasarı ilet
        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        // Vurma işlemi biter bitmez mermiyi kapat
        DeactivateBullet();
    }

    private void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }
}