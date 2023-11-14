using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBorderComing : MonoBehaviour
{
    public float borderSpeed;
    public Vector3 respawnPos  = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(borderSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetPosition()
    {
        gameObject.transform.position = respawnPos;
    }
}
