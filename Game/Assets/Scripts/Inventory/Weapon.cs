using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [SerializeField] float damage = 10;
    [SerializeField] float range = 3;
    [SerializeField] GameObject prefab;

    public GameObject GetPrefab()
    {
        return prefab;
    }
}
