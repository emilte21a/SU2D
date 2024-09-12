using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private Collider2D _collider;
    private bool _isPlayerOnPlatform;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (_isPlayerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
        {
            _collider.enabled = false;
            StartCoroutine(EnableCollider());
        }

    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<MovementController>();

        if (player != null) _isPlayerOnPlatform = value;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, true);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, false);
    }
}
