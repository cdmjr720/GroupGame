using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    //Script to keep track on player inventory 
    //Might be an array of game objects with a quantity held 


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
            if (items[i] == itemPickedUp && numHeld[i] < items[i].GetMaxHeld())
            {
                numHeld[i] += quantity;
                Debug.Log(this.ToString());
                return;
            }
        }
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemPickedUp;
                numHeld[i] = quantity;
                Debug.Log(this.ToString());
                return;
            }
        }
        Debug.Log("Full Inventory");
    }
}
