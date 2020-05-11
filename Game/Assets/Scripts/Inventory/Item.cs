using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //This script will be attached to any items the player can pickup 
    //it should keep track of how many of this item the player can hold


    [SerializeField] int maxHeld = 10;
    [SerializeField] string itemName = "";

    [SerializeField] bool isFood = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetName()
    {
        return itemName;
    }

    public int GetMaxHeld()
    {
        return maxHeld;
    }

    public bool IsFood()
    {
        return isFood;
    }
}
