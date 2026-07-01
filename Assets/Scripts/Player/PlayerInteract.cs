using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Etkileşim Ayarları")]
    [Tooltip("Oyuncunun objelerle etkileşime girebileceği maksimum mesafe")]
    public float interactRange = 2.5f;
    
    [Tooltip("Sadece bu katmandaki objelerle etkileşime girilsin (Performans için)")]
    public LayerMask interactableLayer;

    private void Update()
    {
        // Oyuncu 'E' tuşuna bastığında
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        // Oyuncunun etrafında görünmez bir küre oluşturup içindeki objeleri buluyoruz
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactRange, interactableLayer);
        
        float closestDistance = Mathf.Infinity;
        IInteractable closestInteractable = null;

        // Bulunan objeler arasında oyuncuya EN YAKIN olanı seçiyoruz
        foreach (Collider col in hitColliders)
        {
            IInteractable interactable = col.GetComponent<IInteractable>();
            
            if (interactable != null)
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }
        }

        // Eğer yakında etkileşime girilebilir bir şey bulduysak, onu çalıştır
        if (closestInteractable != null)
        {
            closestInteractable.Interact();
        }
    }

    // Seçilen etkileşim alanını Unity Editöründe görmek için çizdiriyoruz
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}