using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rbPlayer;
    GameObject focalPoint;
    Renderer matPlayer;
    public float speed = 10f;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        matPlayer = GetComponent<Renderer>();
    }

    void Update()
    {
        //This makes the ball roll
        float forwardInput = Input.GetAxis("Vertical");
        float magnitude = forwardInput * speed * Time.deltaTime;
        rbPlayer.AddForce(focalPoint.transform.forward * magnitude, ForceMode.Impulse);
        
        //This causes the color of the ball to change
        if(forwardInput > 0)
            matPlayer.material.color = new Color(1.0f - forwardInput, 1.0f, 1.0f - forwardInput);
        else
            matPlayer.material.color = new Color(1.0f + forwardInput, 1.0f, 1.0f + forwardInput);
    }
}
