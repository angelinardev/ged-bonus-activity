using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public int health = 5;

    //one instance
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    public void ChangeHealth(int healthValue)
    {
        health += healthValue;
        Debug.Log("Health:" + health);
       
    }
   void Update() 
   {
        //health drops below 0
        if (health <=0)
        {
            Application.Quit();
            
        }
    }
}
