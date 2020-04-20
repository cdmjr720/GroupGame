using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    //Script to keep track on player inventory 
    //Might be an array of game objects with a quantity held 

    //inventory slots in UI
    [SerializeField] GameObject[] inventorySlots;
    [SerializeField] Text[] inventoryText;

    //array of items in inventory 
    Item[] items = new Item[5];
    //array of number of items held 
    int[] numHeld = new int[5];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            items[i] = null;
            numHeld[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //to string method for debug
    //no effect on game 
    public override string ToString()
    {
        string str = "";
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                continue;
            }
            str += "item " + (i+1).ToString() + ": " + items[i].GetName() + "        ";
            str += "quantity: " + numHeld[i];
        }
        str += "\n\n";
        return str;
    }

    public void AddItem(Item itemPickedUp, int quantity)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemPickedUp && numHeld[i] < items[i].GetMaxHeld()) //if already holding that type with more room
            {
                numHeld[i] += quantity; //pick up 
                inventoryText[i].text = numHeld[i].ToString(); //update text 
                return;
            }
        }
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) //empty slot 
            {
                items[i] = itemPickedUp; //set the type to the item
                inventorySlots[i].GetComponent<Image>().sprite = itemPickedUp.GetComponent<SpriteRenderer>().sprite; //change picture 
                numHeld[i] = quantity; //add to number held 
                inventoryText[i].text = numHeld[i].ToString(); //update text
                return;
            }
        }
        Debug.Log("Full Inventory");
    }
}
