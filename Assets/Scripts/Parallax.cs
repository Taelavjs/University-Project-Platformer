using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform transCamera;
    Vector3 startPos;
    [SerializeField]
    float parallaxValue;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        float distance = transCamera.position.x * parallaxValue;

        transform.position = new Vector3(distance + startPos.x, transform.position.y, transform.position.z);
    }
}
