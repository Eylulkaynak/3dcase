using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform target;

    public float distance = 6f;
    public float height = 3f;
    
    [Tooltip("Kameranın pozisyon takip hızı")]
    public float positionSmoothSpeed = 10f;
    
    [Tooltip("Kameranın oyuncuya dönme hızı")]
    public float rotationSmoothSpeed = 10f;

    private void Start()
    {
        // Fareyi ekrana kilitler ve gizler
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // 1. İstenilen Pozisyon: Oyuncunun her zaman 'distance' kadar arkasında ve 'height' kadar yukarısında
        Vector3 desiredPosition = target.position - (target.forward * distance) + (Vector3.up * height);

        // 2. Pozisyonu Yumuşatma (Lerp ile kamerayı oyuncunun arkasına pürüzsüzce çekiyoruz)
        transform.position = Vector3.Lerp(transform.position, desiredPosition, positionSmoothSpeed * Time.deltaTime);

        // 3. İstenilen Dönüş: Kameranın bakacağı hedef (Oyuncunun merkezi yerine biraz daha üstü, kafa hizası)
        Vector3 lookTarget = target.position + Vector3.up * 1.5f;

        // 4. Dönüşü Yumuşatma (Kamerayı hedefe doğru pürüzsüzce döndürüyoruz)
        Quaternion desiredRotation = Quaternion.LookRotation(lookTarget - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
    }
}