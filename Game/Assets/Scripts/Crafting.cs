using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    //This script is used for crafting shelter and weapons

    //items used for crafting 
    [SerializeField] Item woodItem;
    [SerializeField] Item rockItem;

    //crafted items 
    [SerializeField] Item swordItem;

    //inventory of player 
    InventorySystem inventory;

    private void Start()
    {
        inventory = FindObjectOfType<InventorySystem>();
    }

    public void CraftSword()
    {
        //TODO check for empty slot

        //sword requires one wood and two rock 
        if (inventory.RemoveItem(woodItem, 1) && inventory.RemoveItem(rockItem, 2))
        {
            inventory.AddItem(swordItem, 1);
        }
    }
}
