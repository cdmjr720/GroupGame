using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    //textures for the tree and then log when tree is cut 
    [SerializeField] GameObject logPile;
    [SerializeField] GameObject tree;
    GameObject logPileInstance;
    GameObject treeInstance;

    //item picked up 
    [SerializeField] Item woodItem;
    //players' inventory 
    InventorySystem inventorySystem;

    //hardness relates to the time it takes to cut down the tree 
    //TODO change this so it is determined by what the character is holding so tools can be used 
    [SerializeField] [Range(1, 10)] float hardness = 9f;

    private float timeLeft;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1);
    }

    private void Start()
    {
        //make the tree visible 
        treeInstance = Instantiate<GameObject>(tree, gameObject.transform);
        inventorySystem = FindObjectOfType<InventorySystem>();
    }

    public void Cut()
    {
        logPileInstance = Instantiate<GameObject>(logPile, gameObject.transform);
        Destroy(treeInstance);
        inventorySystem.AddItem(woodItem, 1);
    }
}
