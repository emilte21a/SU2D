using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BunkerConfig : MonoBehaviour
{
    [SerializeField] TMP_Text healthPointDisplay;
    [SerializeField] Transform spawnPoint;
    [SerializeField] public float HP = 100;
    [SerializeField] AudioClip collectiblePickupSound;
    [SerializeField] AudioClip onDeathSound;
    [SerializeField] AudioClip[] takingDamageSound = new AudioClip[2];
    private InventoryController inventoryController;

    private int _maxFallSpeedBeforeTakingDamage = -26;

    Vector2 latestVelocity;

    void Start()
    {
        transform.position = spawnPoint.position;
        inventoryController = GetComponent<InventoryController>();
        HP = 100;
    }

    void Update()
    {
        if (GetComponent<MovementController>().IsGrounded())
        {
            if (latestVelocity.y < _maxFallSpeedBeforeTakingDamage)
            {
                HP -= Mathf.Abs((int)latestVelocity.y * 2 - _maxFallSpeedBeforeTakingDamage);
                GetComponent<AudioSource>().PlayOneShot(takingDamageSound[(int)Random.Range(0, 2)]);
            }

        }

        latestVelocity = GetComponent<Rigidbody2D>().velocity;


        if (HP <= 0)
        {
            GetComponent<AudioSource>().PlayOneShot(onDeathSound);
            transform.position = spawnPoint.position;
            inventoryController.itemsInInventory.Clear();
            HP = 100;
        }

        healthPointDisplay.text = $"{HP}";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            if (inventoryController != null)
            {
                GetComponent<AudioSource>().PlayOneShot(collectiblePickupSound);
                inventoryController.AddItemToInventory(ItemType.MechanicalPart, 1);
                other.gameObject.SetActive(false);
            }
        }
    }
}
