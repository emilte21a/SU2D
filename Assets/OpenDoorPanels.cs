using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDoorPanels : MonoBehaviour
{
    [SerializeField] GameObject leftPanel;
    [SerializeField] GameObject rightPanel;
    [SerializeField] Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player"))
            animator.SetBool("ShouldOpen", true);
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player"))
            animator.SetBool("ShouldOpen", false);
    }

}
