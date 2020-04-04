﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    //textures for the tree and then log when tree is cut 
    [SerializeField] GameObject logPile;
    [SerializeField] GameObject tree;
    GameObject logPileInstance;
    GameObject treeInstance;

    //hardness relates to the time it takes to cut down the tree 
    //TODO change this so it is determined by what the character is holding so tools can be used 
    [SerializeField] [Range(1, 10)] float hardness = 9f;

    private float timeLeft;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    private void Start()
    {
        //make the tree visible 
        treeInstance = Instantiate<GameObject>(tree, gameObject.transform);
    }

    public void Cut()
    {
        logPileInstance = Instantiate<GameObject>(logPile, gameObject.transform);
        Destroy(treeInstance);
    }
}
