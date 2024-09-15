using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneInfoHolder : MonoBehaviour
{
    [SerializeField] public SceneInformation sceneInfo;

    [SerializeField] CurrentScene currentScene;
    void Start()
    {
        sceneInfo.currentScene = currentScene;
    }
}
