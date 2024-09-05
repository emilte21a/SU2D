using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableThisObject : MonoBehaviour
{
    public void DisableObject()
    {
        transform.gameObject.SetActive(false);
    }


}
