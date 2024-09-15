using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Movement Controls")]
    [SerializeField] float movementSpeed = 10;
    [SerializeField] float jumpForce = 10;
    [HideInInspector] public short lastDirection = 1;

    [Header("Important stuff")]
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] ParticleSystem dust;
    [SerializeField] GameObject spriteHolder;

    [Header("Ground Check")]
    [SerializeField] float castDistance = 1;
    [SerializeField] Vector2 boxSize;
    [SerializeField] LayerMask layerMask;

    private readonly float _coyoteTime = 0.4f;
    private float _coyoteTimeCounter;

    private readonly float _bufferTime = 0.4f;
    private float _bufferCounter;

    Animator animator;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip jumpSound;


    bool isCollidingWithDoor;
    bool isCollidingWithAntenna;

    [Header("Misc")]
    [SerializeField] TMP_Text errorMessage;
    [SerializeField] int itemCount;
    SceneInformation sceneInformation;

    void Start()
    {
        animator = GetComponent<Animator>();

        rigidbody2D = GetComponent<Rigidbody2D>();

        rigidbody2D.freezeRotation = true;

        sceneInformation = GetComponent<SceneInfoHolder>().sceneInfo;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.C)) transform.localScale = new Vector3(1.5f, 0.8f, 1.5f); //Crouch
        else transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        if (isCollidingWithAntenna && Input.GetKeyDown(KeyCode.F))
            SceneManager.LoadScene("Director");


        switch (isCollidingWithDoor)
        {
            case true:
                if (sceneInformation.currentScene == CurrentScene.Moon && Input.GetKeyDown(KeyCode.F))
                    SceneManager.LoadScene("moonBunker");

                else if (Input.GetKeyDown(KeyCode.F) && sceneInformation.currentScene == CurrentScene.MoonBunker)
                {
                    if (InventoryController.itemsInInventory.Count >= 2)
                        SceneManager.LoadScene("Moon");

                    else if (InventoryController.itemsInInventory.Count < 2)
                    {
                        errorMessage.text = "YOU HAVE NOT GATHERED ENOUGH PARTS...";
                        StartCoroutine(ErrorMessageDisplay());
                    }
                }
                break;
        }

        #region movement

        float horizontalMovement = Input.GetAxisRaw("Horizontal");

        rigidbody2D.velocity = new Vector2(horizontalMovement * movementSpeed, rigidbody2D.velocity.y);

        if (rigidbody2D.velocity.x > 0 || rigidbody2D.velocity.x < 0)
            animator.SetBool("IsRunning", true);

        else
            animator.SetBool("IsRunning", false);


        if (IsGrounded())
            _coyoteTimeCounter = _coyoteTime;

        else
            _coyoteTimeCounter -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)) _bufferCounter = _bufferTime;
        else _bufferCounter -= Time.deltaTime;


        if (_bufferCounter > 0 && _coyoteTimeCounter > 0) //Här är själva hoppet
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(jumpSound);
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
        #endregion
    }

    public bool IsGrounded()
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


    IEnumerator ErrorMessageDisplay()
    {
        errorMessage.enabled = true;
        yield return new WaitForSeconds(2);
        errorMessage.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Building":
                isCollidingWithDoor = true;
                break;

            case "Antenna":
                isCollidingWithAntenna = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Building":
                isCollidingWithDoor = false;
                break;

            case "Antenna":
                isCollidingWithAntenna = false;
                break;
        }
    }


}
