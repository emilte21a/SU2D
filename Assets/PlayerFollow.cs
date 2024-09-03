using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] GameObject gameObjectToFollow;

    [SerializeField, Range(0, 1)]
    float smoothTime = 0.1f;

    private Vector3 zero = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(gameObjectToFollow.transform.position.x, 0, -10), ref zero, smoothTime);


    }
}
