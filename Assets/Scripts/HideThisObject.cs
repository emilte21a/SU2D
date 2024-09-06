using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideThisObject : MonoBehaviour
{
    public void DisableObject()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }


}
