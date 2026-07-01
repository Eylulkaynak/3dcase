using UnityEngine;
using Unity.Cinemachine; 

public class ZoomZone : MonoBehaviour
{
    [Header("Kamera Ayarları")]
    [Tooltip("Yakınlaşacak olan İKİNCİ sanal kamerayı (Zoom Camera) buraya sürükle")]
    public CinemachineCamera zoomCamera; 

    private void OnTriggerEnter(Collider other)
    {
        // Bölgeye giren obje Player ise zoom kamerasını aktifleştir
        if (other.CompareTag("Player") && zoomCamera != null)
        {
            zoomCamera.Priority = 20; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Player bölgeden çıkınca zoom kamerasını devre dışı bırakıp ana kameraya dön
        if (other.CompareTag("Player") && zoomCamera != null)
        {
            zoomCamera.Priority = 0; 
        }
    }
}