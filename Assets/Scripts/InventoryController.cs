using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    [SerializeField] TMP_Text mechPartAmountDisplay;
    [SerializeField] TMP_Text antiVirusVialAmountDisplay;

    public static Dictionary<ItemType, int> itemsInInventory;

    public int maxItems = 6;

    void Start()
    {
        itemsInInventory = GetComponent<SceneInfoHolder>().sceneInfo.itemsInInventory;
    }

    void Update()
    {
        if (itemsInInventory.Count > 0)
        {
            foreach (var kvp in itemsInInventory)
            {
                if (itemsInInventory.Count > 0)
                {
                    if (kvp.Key == ItemType.MechanicalPart)
                        mechPartAmountDisplay.text = $" {itemsInInventory[kvp.Key]}/" + maxItems;

                    else
                        antiVirusVialAmountDisplay.text = $"{itemsInInventory[kvp.Key]}/" + 1;
                }
            }
        }
        else
        {
            mechPartAmountDisplay.text = $"0/" + maxItems;
            antiVirusVialAmountDisplay.text = $"0/" + 1;

        }
    }

    public void AddItemToInventory(ItemType itemType, int quantity)
    {
        if (itemsInInventory.Count > 0)
        {
            bool itemAlreadyExists = itemsInInventory.Any(item => item.Key == itemType);

            if (itemAlreadyExists)
            {
                itemsInInventory[itemType] += quantity;
            }
            else
            {
                itemsInInventory.Add(itemType, 1);
            }

        }
        else
        {
            itemsInInventory.Add(itemType, 1);
        }
    }
}
