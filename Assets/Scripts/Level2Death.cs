using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Death : MonoBehaviour
{
    public Transform deathBorderPos;
    public DeathBorderComing deathBorder;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            deathBorder.resetPosition();
        }

        if (collision.gameObject.CompareTag("Snow"))
        {
            deathBorder.respawnPos = collision.gameObject.transform.parent.transform.position;
        }
    }

}
