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
    [Header("Audio")]
    [SerializeField] AudioClip collectiblePickupSound;
    [SerializeField] AudioClip onDeathSound;
    [SerializeField] AudioClip[] takingDamageSound = new AudioClip[2];

    [Header("CollectibleHandler")]
    [SerializeField] MechanicalPartHandler mechanicalPartHandler;
    private InventoryController inventoryController;

    private int _maxFallSpeedBeforeTakingDamage = -26;

    Vector2 latestVelocity;

    bool playerIsElectrocuted = false;

    Timer timer = new Timer(1000);

    void Start()
    {
        transform.position = spawnPoint.position;
        inventoryController = GetComponent<InventoryController>();
        HP = 100;
        timer.Elapsed += (sender, e) =>
        {
            HP -= 12;
            GetComponent<AudioSource>().PlayOneShot(takingDamageSound[(int)Random.Range(0, 2)]);
        };
    }

    void Update()
    {
        // GetComponent<MovementController>().sceneInfo.itemsInInventory = InventoryController.itemsInInventory;
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

        if (playerIsElectrocuted) timer.Start();

        else timer.Stop();

        //Om spelaren dör damage (är glitchig som bara den)
        if (HP <= 0)
        {
            GetComponent<AudioSource>().PlayOneShot(onDeathSound);
            transform.position = spawnPoint.position;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            InventoryController.itemsInInventory.Clear();
            mechanicalPartHandler.InitializeCollectibles();
            HP = 100;
        }

        healthPointDisplay.text = $"{HP}";
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        switch (other.tag)
        {
            case "Collectible":
                if (inventoryController != null)
                {
                    GetComponent<AudioSource>().PlayOneShot(collectiblePickupSound);
                    inventoryController.AddItemToInventory(other.GetComponent<ItemObject>().inventoryItem.itemType, 1);
                    other.gameObject.SetActive(false);
                }
                break;

            case "Killzone":
                GetComponent<BunkerConfig>().HP = 0;
                break;

            case "ElectricWire":
                playerIsElectrocuted = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ElectricWire"))
        {
            playerIsElectrocuted = false;
        }
    }
}
