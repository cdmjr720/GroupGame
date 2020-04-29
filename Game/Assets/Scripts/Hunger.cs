using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    [SerializeField] float hunger = 100f;
    [SerializeField] RectTransform foreground = null;

    [SerializeField] float timeBetweenHungerIncrease = 10f;

    private float timeSinceHungerIncrease;

    // Update is called once per frame
    void Update()
    {
        foreground.localScale = new Vector3(GetFraction(), 1, 1);
        if (timeSinceHungerIncrease > timeBetweenHungerIncrease)
        {
            //lower hunger bar
            hunger -= 5;
            timeSinceHungerIncrease = 0;
        }
        timeSinceHungerIncrease += Time.deltaTime;
    }

    public float GetFraction()
    {
        return hunger / 100;
    }

    public void EatFood(Food food)
    {
        hunger += food.GetHungerValue();
    }
}
