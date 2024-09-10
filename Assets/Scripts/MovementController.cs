using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] WorldGeneration worldGeneration;
    [SerializeField] InventoryController inventoryController;

    [Header("Movement Controls")]
    [SerializeField] float movementSpeed = 10;
    [SerializeField] float jumpForce = 10;
    [SerializeField] public short lastDirection = 1;
    [SerializeField] float playerHeight;

    [Header("Important stuff")]
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] ParticleSystem dust;
    [SerializeField] GameObject spriteHolder;

    [Header("Ground Check")]
    [SerializeField] float castDistance = 1;
    [SerializeField] Vector2 boxSize;
    [SerializeField] LayerMask layerMask;


    private Vector3 _spawnPos;

    private readonly float _coyoteTime = 0.2f;
    private float _coyoteTimeCounter;

    private readonly float _bufferTime = 0.2f;
    private float _bufferCounter;

    Animator animator;

    void Start()
    {
        inventoryController = GetComponent<InventoryController>();
        animator = GetComponent<Animator>();
        playerHeight = GetComponentInChildren<SpriteRenderer>().bounds.size.y + 1;
        rigidbody2D = GetComponent<Rigidbody2D>();

        if (worldGeneration.spawnPoints != null && worldGeneration.spawnPoints.Length > 0)
        {
            _spawnPos = new Vector3((int)worldGeneration.spawnPoints[worldGeneration.worldSize / 2].x, (int)worldGeneration.spawnPoints[worldGeneration.worldSize / 2].y + playerHeight);
            transform.position = _spawnPos;
        }

        rigidbody2D.freezeRotation = true;
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");

        rigidbody2D.velocity = new Vector2(horizontalMovement * movementSpeed, rigidbody2D.velocity.y);


        if (rigidbody2D.velocity.x > 0 || rigidbody2D.velocity.x < 0)
        {
            Debug.Log("RUNNING!");
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        //Logik gällande Jump Buffering och Coyote Time
        if (IsGrounded()) _coyoteTimeCounter = _coyoteTime;

        else _coyoteTimeCounter -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)) _bufferCounter = _bufferTime;
        else _bufferCounter -= Time.deltaTime;


        if (_bufferCounter > 0 && _coyoteTimeCounter > 0)
        {
            CreateDust();
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            _bufferCounter = 0;
        }

        //Gör det möjligt för mindre hopp om man lätt "tapar" på space och större hopp om man håller ned
        else if (Input.GetKeyUp(KeyCode.Space) && rigidbody2D.velocity.y > 0)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 0.6f);
            _coyoteTimeCounter = 0f;
        }

        //Gör endast så att spelaren faller snabbare
        if (rigidbody2D.velocity.y < 0 && !Input.GetKey(KeyCode.Space))
        {
            rigidbody2D.gravityScale = 2.5f;
            //rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 1.02f);
        }
        else if (rigidbody2D.velocity.y < 0 && Input.GetKey(KeyCode.Space)) rigidbody2D.gravityScale = 2f;

        else rigidbody2D.gravityScale = 1;

        if (rigidbody2D.velocity.x > 0) lastDirection = 1;
        else if (rigidbody2D.velocity.x < 0) lastDirection = -1;

        if (lastDirection == 1) spriteHolder.transform.eulerAngles = new Vector3(0, 0, 0); // normal riktning
        else if (lastDirection == -1) spriteHolder.transform.eulerAngles = new Vector3(0, 180, 0); // flippad riktning
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, layerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private void CreateDust()
    {
        dust.Play();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            inventoryController.AddItemToInventory(ItemType.MechanicalPart, 1);
            other.gameObject.SetActive(false);
        }
    }
}
