using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SceneInformationScriptableObject", order = 1)]
public class SceneInformation : ScriptableObject
{
    public CurrentScene currentScene = CurrentScene.Moon;
    public Dictionary<ItemType, int> itemsInInventory = new Dictionary<ItemType, int>();
}

