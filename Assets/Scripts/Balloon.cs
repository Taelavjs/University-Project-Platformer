using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public float damage { get; set; }
    public ParticleSystem popVfx;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            popVfx.Play();
            SpriteRenderer sprender = gameObject.GetComponent<SpriteRenderer>();
            sprender.enabled = false;
            BoxCollider2D bCol = gameObject.GetComponent<BoxCollider2D>();
            bCol.enabled = false;
            Destroy(gameObject, 2f);


        }

        
    }



}
