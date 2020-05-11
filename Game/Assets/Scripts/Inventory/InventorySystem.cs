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
    [SerializeField] GameObject weaponSlot;

    //array of items in inventory 
    Item[] items = new Item[5];
    //weapon item thing 
    Item weapon = null;
    //array of number of items held 
    int[] numHeld = new int[5];

    //weapon being held 
    GameObject weaponHeld;

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
        //if item is food eat it instead
        if (itemPickedUp.IsFood())
        {
            //eat 
            Food foodItem = (Food)itemPickedUp;
            FindObjectOfType<PlayerController>().GetComponent<Hunger>().EatFood(foodItem);
        } else
        {
            //pick up 
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
        }
    }


    //removes items for crafting 
    public void RemoveItem(Item item, int quantity)
    {
        for (int i = 0; i < items.Length; i++)
        {
            //if holding that item 
            if (items[i] == item && numHeld[i] >= quantity)
            {
                numHeld[i] -= quantity; // remove items 
                if (numHeld[i] == 0) //out of items 
                {
                    items[i] = null;
                    inventorySlots[i].GetComponent<Image>().sprite = null;
                    inventoryText[i].text = "";
                } else
                {
                    inventoryText[i].text = numHeld[i].ToString();
                }
            }
        }
    }

    public bool HasEnough(Item item, int quantity)
    {
        //loop through inventory 
        for (int i = 0; i < items.Length; i++)
        {
            if (item == items[i])
            {
                if (numHeld[i] >= quantity)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }
        return false;
    }

    public void AddWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weaponSlot.GetComponent<Image>().sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        
        weaponHeld = Instantiate(weapon.GetPrefab(), FindObjectOfType<PlayerController>().GetHandLocation());
    }
}
