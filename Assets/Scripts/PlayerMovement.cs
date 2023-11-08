using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rbPlayer;

    [SerializeField]
    private float normalSpeed = 5f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private bool isGrounded = false;
    private bool isJumping = false;
    [SerializeField]
    private float fallForce = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rbPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }

        if(rbPlayer.velocity.y < 2f && isJumping == true)
        {
            Debug.Log("Woow");
            rbPlayer.AddForce(Vector2.down * fallForce * Time.deltaTime, ForceMode2D.Force);
        }

        rbPlayer.transform.Translate(Input.GetAxis("Horizontal") * normalSpeed * Vector2.right * Time.deltaTime);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
