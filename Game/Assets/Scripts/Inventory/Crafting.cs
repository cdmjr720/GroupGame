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
    [SerializeField] Weapon swordItem;

    //inventory of player 
    InventorySystem inventory;

    //fire game object
    [SerializeField] Fire fireInteractable;
    Fire fireInstance;

    //player 
    PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        inventory = FindObjectOfType<InventorySystem>();
    }

    public void CraftSword()
    {
        //sword requires one wood and two rock 
        if (inventory.HasEnough(woodItem, 1) && inventory.HasEnough(rockItem, 2))
        {
            inventory.RemoveItem(woodItem, 1);
            inventory.RemoveItem(rockItem, 2);
            inventory.AddWeapon(swordItem);
        }
    }

    public void CarftFire()
    {
        //fire requires 2 wood and 2 stone 
        if (inventory.HasEnough(woodItem, 2) && inventory.HasEnough(rockItem, 2))
        {
            inventory.RemoveItem(woodItem, 2);
            inventory.RemoveItem(rockItem, 2);

            fireInstance = Instantiate<Fire>(fireInteractable, player.transform.position + ((Vector3.down * 3)/2) , player.transform.rotation);
            fireInstance.CreateFirePit();
        }
    }
}
