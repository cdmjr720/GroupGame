using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    //textures for the tree and then log when tree is cut 
    [SerializeField] Mesh logMesh;
    [SerializeField] Material logMaterial;
    [SerializeField] Mesh treeMesh;
    [SerializeField] Material treeMaterial;

    //hardness relates to the time it takes to cut down the tree 
    //TODO change this so it is determined by what the character is holding so tools can be used 
    [SerializeField] [Range(1, 10)] float hardness = 3f;
    private float timeLeft;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    private void Start()
    {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshFilter.mesh = treeMesh;
        meshRenderer.material = treeMaterial;
    }

    public void Cut()
    {
        timeLeft = hardness;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        meshFilter.mesh = logMesh;
        meshRenderer.material = treeMaterial;
    }
}
