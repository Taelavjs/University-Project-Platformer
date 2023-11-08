using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private Transform transPlayer;
    public float maxSpeed;
    public float smoothTime;
    public float xOffset;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        transPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector3 cameraPosition = new Vector3(transPlayer.position.x + xOffset, 0f, gameObject.transform.position.z);
        Vector3 cleanCamera = Vector3.SmoothDamp(transform.position, cameraPosition, ref velocity,smoothTime, maxSpeed);
        transform.position = cleanCamera;
    }
}
