using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    [SerializeField] BunkerConfig bunkerConfig;
    [SerializeField] Slider slider;

    void Start()
    {
        slider.maxValue = 100;
        slider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = bunkerConfig.HP;
    }
}
