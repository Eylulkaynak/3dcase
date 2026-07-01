using UnityEngine;

public class SwingingDoor : MonoBehaviour, IInteractable
{
    [Header("Door Settings")]
    [Tooltip("Kapının kaç derece açılacağını belirler (Örn: 90 veya -90)")]
    public float openAngle = 90f;
    
    [Tooltip("Kapının açılma/kapanma hızı.")]
    public float rotationSpeed = 5f;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Quaternion targetRotation;
    
    private bool isOpen = false;

    private void Start()
    {
        // Oyun başladığında kapının ilk durduğu açıyı "kapalı" olarak kaydet
        closedRotation = transform.rotation;
        
        // Kapının "açık" açısını hesapla (Y ekseninde belirlenen derece kadar döndür)
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
        
        // Başlangıç hedefi kapalı kalması
        targetRotation = closedRotation;
    }

    public void Interact()
    {
        isOpen = !isOpen; 
        
        if (isOpen)
        {
            targetRotation = openRotation;
            Debug.Log($"<b>{gameObject.name}</b> açılıyor.");
        }
        else
        {
            targetRotation = closedRotation;
            Debug.Log($"<b>{gameObject.name}</b> kapanıyor.");
        }
    }

    private void Update()
    {
        // Kapı hedefine ulaşana kadar pürüzsüz bir şekilde (Slerp) o açıya dön
        if (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}