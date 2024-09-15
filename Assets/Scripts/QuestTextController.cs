using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestTextController : MonoBehaviour
{

    [SerializeField] TMP_Text questText;

    CurrentScene currentScene;

    void Start()
    {
        currentScene = GetComponent<SceneInfoHolder>().sceneInfo.currentScene;
    }

    void Update()
    {
        int mechCount = GetComponent<SceneInfoHolder>().sceneInfo.itemsInInventory[ItemType.MechanicalPart] + GetComponent<SceneInfoHolder>().sceneInfo.itemsInInventory[ItemType.AntiVirus];

        if (mechCount >= 7)
        {
            if (currentScene == CurrentScene.MoonBunker)
            {
                questText.text = ">Head back to the surface";
            }
            else if (currentScene == CurrentScene.Moon)
            {
                questText.text = ">Proceed to the Antenna and press F to head back to orbit";
            }
            else if (currentScene == CurrentScene.Director)
            {
                questText.text = ">Travel to the earth";
            }

            else if (currentScene == CurrentScene.Earth)
            {
                questText.text = "> Head right to the big antenna and undo what has plagued humanity";
            }
        }

    }
}
