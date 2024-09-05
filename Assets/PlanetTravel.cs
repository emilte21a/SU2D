using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetTravel : MonoBehaviour
{
    // This method will be called from the button with the planet and loading scene as parameters
    public void BeginTravel(string planetName)
    {
        // Store the planet name in PlayerPrefs (or any other persistent storage)
        PlayerPrefs.SetString("PlanetDestination", planetName); // Save the destination

        // Load the loading screen scene
        SceneManager.LoadScene("LoadingScene");
    }
}
