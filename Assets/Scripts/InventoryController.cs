using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public TMP_Text textDisplay;

    public static Dictionary<ItemType, int> itemsInInventory;

    public int maxItems = 7;

    void Start()
    {
        itemsInInventory = new Dictionary<ItemType, int>();
    }

    void Update()
    {
        if (itemsInInventory.Count > 0)
        {

            foreach (var kvp in itemsInInventory)
            {
                if (itemsInInventory.Count > 0)
                    textDisplay.text = $" {itemsInInventory[kvp.Key]}/" + maxItems;
            }
        }
        else
            textDisplay.text = $"0/" + maxItems;
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
