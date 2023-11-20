using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashLvl23 : MonoBehaviour
{
    Rigidbody2D rbPlayer;
    public float dashForce;
    private PlayerMovement playerMovement;
    private float dashCooldown = 3f;
    private float oldSpeed;
    private float oldCd;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        oldSpeed = playerMovement.maxSpeed;
        oldCd = dashCooldown;

    }

    // Update is called once per frame
    void Update()
    {
        dashCooldown -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && dashCooldown < 0)
        {
            StartCoroutine(dashAttack());
            dashCooldown = oldCd; 
        }
    }
    public Color dashColour;
    IEnumerator dashAttack()
    {
        var color = gameObject.GetComponent<SpriteRenderer>().color;

        rbPlayer.velocity = new Vector3(dashForce * Input.GetAxisRaw("Horizontal"), rbPlayer.velocity.y, 0f);
        playerMovement.maxSpeed = dashForce;
        gameObject.GetComponent<SpriteRenderer>().color = dashColour;
        yield return new WaitForSeconds(0.5f);
        playerMovement.maxSpeed = oldSpeed;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }


}
