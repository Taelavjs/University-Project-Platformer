using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    private float desiredAlpha;
    private float currentAlpha;
    // Start is called before the first frame update
    void Start()
    {
        desiredAlpha = 0.0f;
        currentAlpha = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
