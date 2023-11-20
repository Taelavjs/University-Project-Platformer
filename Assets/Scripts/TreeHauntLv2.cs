using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHauntLv2 : MonoBehaviour
{

    public SpriteRenderer hauntImage;
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
        if (collision.gameObject.CompareTag("Haunt")){
            hauntImage.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Haunt")){ 
            hauntImage.gameObject.SetActive(false);
        }
    }
}
