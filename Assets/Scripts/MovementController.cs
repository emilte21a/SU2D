using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Movement Controls")]
    [SerializeField] float movementSpeed = 5;
    [SerializeField] float jumpForce = 10;
    [SerializeField] short lastDirection = 1;

    [Header("blabla")]
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] ParticleSystem dust;

    [Header("Ground Check")]
    public float castDistance = 1;
    public Vector2 boxSize;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            Jump();

        if (rigidbody2D.velocity.x > 0) lastDirection = 1;
        else if (rigidbody2D.velocity.x < 0) lastDirection = -1;

        if (lastDirection == 1) transform.eulerAngles = new Vector3(0, 0, 0); // normal
        else if (lastDirection == -1) transform.eulerAngles = new Vector3(0, 180, 0); // normal
    }

    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        rigidbody2D.velocity = new Vector2(horizontalMovement * movementSpeed, rigidbody2D.velocity.y);
    }

    private bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, layerMask))
            return true;

        return false;
    }

    private void Jump()
    {
        CreateDust();
        rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private void CreateDust()
    {
        dust.Play();
    }
}
