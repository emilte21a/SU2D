using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalPartHandler : MonoBehaviour
{
    public List<Transform> mechanicalPartsPositions;
    public List<GameObject> mechanicalParts;

    [SerializeField] GameObject mechanicalPartPrefab;
    [SerializeField] GameObject vialPrefab;

    [SerializeField] Transform vialPosition;

    // Start is called before the first frame update
    void Start()
    {
        InitializeCollectibles();
    }

    public void InitializeCollectibles()
    {
        mechanicalParts.Clear();
        for (int i = 0; i < mechanicalPartsPositions.Count; i++)
        {
            GameObject mechPart = Instantiate(mechanicalPartPrefab, mechanicalPartsPositions[i].position, mechanicalPartsPositions[i].rotation);
            mechPart.transform.SetParent(mechanicalPartsPositions[i]);
            mechanicalParts.Add(mechPart);
        }
        GameObject vial = Instantiate(vialPrefab, vialPosition.position, vialPosition.rotation);
        vial.transform.SetParent(vialPosition);
        mechanicalParts.Add(vial);
    }

}
