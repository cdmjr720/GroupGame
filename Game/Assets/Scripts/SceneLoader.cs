using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //function to load next scene
    public void LoadNextScene()
    {
        //creates the scene index as an int
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //calls the next active scene
        SceneManager.LoadScene(currentSceneIndex + 1);     
    }

    //function to load main menu
    public void LoadMenu()
    {
        //creates the scene index as an int
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //calls scene 0 (main menu)
        SceneManager.LoadScene(0);
    }

}
