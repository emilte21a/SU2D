using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMousePosition : MonoBehaviour
{
    [SerializeField] bool hideMouse = false;

    // Update is called once per frame
    void Update()
    {
        if (hideMouse) Cursor.visible = false;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = mousePosition;
    }
}
