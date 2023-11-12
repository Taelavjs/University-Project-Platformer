using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public float damage { get; set; }
    public ParticleSystem popVfx;

    BoxCollider2D bCol;
    SpriteRenderer sprender;
    // Start is called before the first frame update
    void Start()
    {
        bCol = gameObject.GetComponent<BoxCollider2D>();
        sprender = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        iTween.ShakePosition(gameObject, new Vector3(0.1f, 0.1f, 0f), 0.1f);

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            popVfx.Play();
            sprender.enabled = false;
            bCol.enabled = false;


        }

        
    }



    public void setActive()
    {
        gameObject.SetActive(true);
        sprender.enabled = true;
        bCol.enabled = true;

    }



}
