using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool hitStun;
    public float hitstunTime;
    // Start is called before the first frame update
    void Start()
    {
        hitStun = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator playerHitStun()
    {
        hitStun = true;
        yield return new WaitForSeconds(hitstunTime);
        hitStun = false;
    }
}
