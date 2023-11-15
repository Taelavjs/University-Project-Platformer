using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        destroySelf();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator destroySelf()
    {
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            collision.gameObject.GetComponent<PlayerHealth>().hit();

            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            Rigidbody2D rigidbody2D = gameObject.transform.GetComponent<Rigidbody2D>();
            BoxCollider2D boxCollider2D = gameObject.transform.transform.GetComponent<BoxCollider2D>();

            spriteRenderer.enabled = false;
            rigidbody2D.Sleep();
            boxCollider2D.enabled = false;
        }


        destroySelf();
    }
}
