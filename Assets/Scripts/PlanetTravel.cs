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
    [SerializeField] float timer = 0;

    string planetDestination;

    void Start()
    {
    }

    public void BeginTravel(string planetName)
    {
        planetDestination = planetName;
        timer = 3;
        audioSource.PlayOneShot(audioClip);
        blackOverlay.GetComponent<Animator>().SetBool("OnDirectorOpen", false);
    }

    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        else if (timer < 0)
        {

            PlayerPrefs.SetString("PlanetDestination", planetDestination);
            UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");
        }
    }
}
