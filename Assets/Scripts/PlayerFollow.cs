using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] GameObject gameObjectToFollow;

    [SerializeField, Range(0, 1)]
    float smoothTime = 0.1f;

    [SerializeField] Vector3 offset;

    private float _initialSmoothing;

    private Vector3 zero = Vector3.zero;

    Vector3 mousePos;

    void Start()
    {
        _initialSmoothing = 0;
        transform.position = new Vector3(gameObjectToFollow.transform.position.x, gameObjectToFollow.transform.position.y + 2, -10);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0f;

        Vector3 direction = (mousePos - gameObjectToFollow.transform.position).normalized;


        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
            _initialSmoothing = smoothTime;

        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(gameObjectToFollow.transform.position.x + gameObjectToFollow.GetComponent<MovementController>().lastDirection, gameObjectToFollow.transform.position.y + 2, -10) + offset + direction, ref zero, _initialSmoothing);
    }

    float timer = 0.5f;
}
