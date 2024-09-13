using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalPartHandler : MonoBehaviour
{
    public static List<Transform> mechanicalPartsPositions;
    public List<GameObject> mechanicalParts;

    [SerializeField] GameObject mechanicalPartPrefab;

    // Start is called before the first frame update
    void Start()
    {
        mechanicalPartsPositions = new List<Transform>();
        mechanicalParts = new List<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < mechanicalParts.Count; i++)
        {
            mechanicalPartsPositions.Add(mechanicalParts[i].transform);
        }
    }
}
