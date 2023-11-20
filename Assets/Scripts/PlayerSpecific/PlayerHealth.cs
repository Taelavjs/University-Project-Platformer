using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            //Dead
        }
    }

    public void hit()
    {
        health -= 1;
        iTween.ShakePosition(gameObject, new Vector3(1f, 1f, 0f), 0.2f);
    }
}
