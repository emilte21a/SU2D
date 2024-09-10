using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class LookAtMouse : MonoBehaviour
{

    public Transform playerPos;
    Vector3 mousePos;
    float flashlightRot;


    void Update()
    {

        mousePos = Input.mousePosition;
        mousePos.z = 0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0f;

        Vector3 direction = (mousePos - playerPos.position).normalized;
        flashlightRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, flashlightRot - 90);

        Debug.DrawLine(playerPos.position, mousePos, Color.red);


    }
}
