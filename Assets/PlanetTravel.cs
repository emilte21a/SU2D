using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlanetTravel : MonoBehaviour
{
    [SerializeField] RawImage blackOverlay;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    float timer = 3;
    float alpha = 0;

    public void BeginTravel(string planetName)
    {
        if (alpha < 255) alpha++;

        audioSource.PlayOneShot(audioClip);
        
        if (timer > 0)
            timer -= Time.deltaTime;


        else if (timer < 0)
        {
            blackOverlay.color = new Color(0, 0, 0, alpha);
            PlayerPrefs.SetString("PlanetDestination", planetName);
            SceneManager.LoadScene("LoadingScene");
        }

    }
}
