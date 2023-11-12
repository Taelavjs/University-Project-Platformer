using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnManager : MonoBehaviour
{
    Balloon[] allChildren;
    // Start is called before the first frame update
    void Start()
    {
        allChildren = GetComponentsInChildren<Balloon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerRespawn() 
    { 
        foreach (Balloon balloon in allChildren)
        {
            balloon.setActive();
        }
    }
}
