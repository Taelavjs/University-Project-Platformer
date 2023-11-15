using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBullets : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clearBullets()
    {
        foreach(GameObject go in GetComponentsInChildren<GameObject>())
        {
            Destroy(go);
        }
    }
}
