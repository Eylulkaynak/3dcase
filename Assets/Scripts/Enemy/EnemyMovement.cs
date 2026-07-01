using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float stopDistance = 1.5f;

    private Transform player;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            direction.Normalize();

            Vector3 targetPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(targetPosition);

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime));
        }
    }
}