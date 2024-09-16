using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MachineController : MonoBehaviour
{

    [SerializeField] Sprite[] machineSprites = new Sprite[3];
    [SerializeField] SceneInfoHolder sceneInfoHolder;
    [SerializeField] TMP_Text instructionText;
    [SerializeField] AudioClip[] machineSounds;

    SpriteRenderer renderer;

    bool isCollidingWithPlayer = false;

    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.sprite = machineSprites[index];

        if (isCollidingWithPlayer)
        {
            instructionText.enabled = true;
            if (sceneInfoHolder.sceneInfo.itemsInInventory.Count >= 2)
            {
                if (index == 0) instructionText.text = "Repair the machine with the parts you've gathered by pressing F";
                if (index == 1) instructionText.text = "Insert the anti-virus by pressing F";
                if (index == 2) instructionText.text = "Spread the antivirus by pressing F";

                if (Input.GetKeyDown(KeyCode.F))
                {

                    if (index < 3) index++;
                

                    if (index < 2) GetComponent<AudioSource>().PlayOneShot(machineSounds[index]);

                    if (index == 3) SceneManager.LoadScene("EndCutscene");
                    
                }
            }
            else
            {
                instructionText.text = "You do not have the required materials";
            }
        }
        else
            instructionText.enabled = false;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) isCollidingWithPlayer = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) isCollidingWithPlayer = false;
    }
}
