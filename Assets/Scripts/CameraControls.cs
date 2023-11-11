using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private Transform transPlayer;
    public float maxSpeed, maxSpeedY;
    public float smoothTime, smoothTimeY;
    public float xOffset, yOffset;

    private Vector3 velocity = Vector3.zero;
    private Vector3 velocityY = Vector3.zero;
    private Rigidbody2D rbPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        transPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private float trueXOffset;

    // Update is called once per frame
    void LateUpdate()
    {
        if (rbPlayer.velocity.x < 0)
        {
            trueXOffset = -xOffset;
        } else if(rbPlayer.velocity.x > 0)
        {
            trueXOffset = xOffset;
        }

        Vector3 cameraPositionx = new Vector3(transPlayer.position.x + trueXOffset, transPlayer.position.y + yOffset, gameObject.transform.position.z);
        Vector3 cleanCamerax = Vector3.SmoothDamp(transform.position, cameraPositionx, ref velocity,smoothTime, maxSpeed);

        Vector3 cameraPositiony = new Vector3(transPlayer.position.x + trueXOffset, transPlayer.position.y + yOffset, gameObject.transform.position.z);
        Vector3 cleanCameray = Vector3.SmoothDamp(transform.position, cameraPositiony, ref velocityY, smoothTimeY, maxSpeedY);
        transform.position = new Vector3(cleanCamerax.x, cleanCameray.y, cleanCameray.z);
    }
}
