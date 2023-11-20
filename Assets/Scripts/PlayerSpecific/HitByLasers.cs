using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByLasers : MonoBehaviour
{

    public float laserHitTimeBuffer = 1f;
    public float originalHitBuffer;
    // Start is called before the first frame update
    void Start()
    {
        originalHitBuffer = laserHitTimeBuffer;
    }

    // Update is called once per frame
    void Update()
    {
        laserHitTimeBuffer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Laser") && laserHitTimeBuffer < 0f)
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<PlayerHealth>().hit();

            laserHitTimeBuffer = originalHitBuffer;
        }
    }

}
