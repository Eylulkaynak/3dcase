using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [Header("Animation Settings")]
    [Tooltip("Drag the Animator component of the chest here.")]
    public Animator chestAnimator;
    
    [Tooltip("The exact name of the OPEN Trigger parameter in the Animator.")]
    public string openTriggerName = "Open";
    
    [Tooltip("The exact name of the CLOSE Trigger parameter in the Animator.")]
    public string closeTriggerName = "Close";

    [Header("Loot Settings")]
    public GameObject lootPrefab;
    public float spawnDistance = 1.2f;

    private bool isOpen = false;
    private bool hasBeenLooted = false;

    private void Start()
    {
        // Try to find the Animator on this object if it wasn't assigned in the Inspector
        if (chestAnimator == null)
        {
            chestAnimator = GetComponent<Animator>();
        }
    }

    public void Interact()
    {
        if (!isOpen)
        {
            OpenChest();
        }
        else
        {
            CloseChest();
        }
    }

    private void OpenChest()
    {
        isOpen = true;
        Debug.Log($"<b>{gameObject.name}</b> has been opened!");

        // Fire the OPEN Trigger
        if (chestAnimator != null)
        {
            chestAnimator.SetTrigger(openTriggerName);
        }

        // Spawn the loot if it hasn't been looted yet
        if (!hasBeenLooted)
        {
            SpawnLoot();
            hasBeenLooted = true; // Mark as looted
        }
    }

    private void CloseChest()
    {
        isOpen = false;
        Debug.Log($"<b>{gameObject.name}</b> has been closed.");

        // Fire the CLOSE Trigger
        if (chestAnimator != null)
        {
            chestAnimator.SetTrigger(closeTriggerName);
        }
    }

    private void SpawnLoot()
    {
        if (lootPrefab != null)
        {
            // Calculate spawn position (slightly in front and above the chest)
            Vector3 spawnPos = transform.position + (transform.forward * spawnDistance) + (Vector3.up * 1f);

            // Instantiate the loot
            GameObject spawnedLoot = Instantiate(lootPrefab, spawnPos, Quaternion.identity);

            // Apply a little physical force to make it pop out
            Rigidbody rb = spawnedLoot.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.forward * 3f + Vector3.up * 5f, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.LogWarning("Loot Prefab is missing on the chest object!");
        }
    }
}