using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] GameObject fireEmpty;
    [SerializeField] GameObject fireOn;
    GameObject fireEmptyInstance;
    GameObject fireOnInstance;
    GameObject player;
    bool fireBuilt = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        
        FireBuild();
        
    }

    private void FireBuild()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            fireEmptyInstance = Instantiate<GameObject>(fireEmpty, gameObject.transform, player.transform);
            fireBuilt = true;
        }
        if (fireBuilt = true && Input.GetKeyDown(KeyCode.F))
        {
            fireOnInstance = Instantiate<GameObject>(fireOn, fireEmpty.transform);

        }
    }

}
