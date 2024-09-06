using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] DialogueController dialogueController;
    [SerializeField] float timeToChangeScene = 5;

    string planetName;

    void Start()
    {
        planetName = PlayerPrefs.GetString("PlanetDestination", "Unknown Planet");
        dialogueController.lines = "Travelling to the " + planetName + "....";

        dialogueController.text = loadingText;
        dialogueController.StartDialogue();
    }


    void Update()
    {
        if (dialogueController.dialogueIsDone)
        {
            if (timeToChangeScene > 0)
                timeToChangeScene -= Time.deltaTime;

            if (timeToChangeScene < 0)
                dialogueController.ChangeScene(planetName);
        }
    }
}
