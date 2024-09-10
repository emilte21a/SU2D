using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public TMP_Text textDisplay;

    public Dictionary<ItemType, int> itemsInInventory;

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
                    textDisplay.text = $" {itemsInInventory[kvp.Key]}/12";
            }
        }
        else
            textDisplay.text = $"0/12";
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
