using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Timers;

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

    bool playerIsElectrocuted = false;

    Timer timer;

    void Start()
    {
        transform.position = spawnPoint.position;
        inventoryController = GetComponent<InventoryController>();
        HP = 100;
    }

    void Update()
    {
        //Om spelaren tar damage (är glitchig som bara den)
        if (GetComponent<MovementController>().IsGrounded())
        {
            if (latestVelocity.y < _maxFallSpeedBeforeTakingDamage)
            {
                HP -= Mathf.Abs((int)latestVelocity.y * 2 - _maxFallSpeedBeforeTakingDamage);
                GetComponent<AudioSource>().PlayOneShot(takingDamageSound[(int)Random.Range(0, 2)]);
            }

        }

        latestVelocity = GetComponent<Rigidbody2D>().velocity;

        if (playerIsElectrocuted)
        {
            timer.Elapsed += (sender, e) => { GetComponent<BunkerConfig>().HP -= 12; };
        }

        //Om spelaren dör damage (är glitchig som bara den)
        if (HP <= 0)
        {
            GetComponent<AudioSource>().PlayOneShot(onDeathSound);
            transform.position = spawnPoint.position;
            InventoryController.itemsInInventory.Clear();
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
                MechanicalPartHandler.mechanicalPartsPositions.Add(other.transform);
                other.gameObject.SetActive(false);
            }
        }

        else if (other.CompareTag("KillZone"))
        {
            GetComponent<BunkerConfig>().HP = 0;
        }

        else if (other.CompareTag("ElectricWire"))
        {
            timer.Start();
            playerIsElectrocuted = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ElectricWire"))
        {
            timer.Stop();
            playerIsElectrocuted = false;
        }
    }
}
