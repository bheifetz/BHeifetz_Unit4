using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rbPlayer;
    GameObject focalPoint;
    Renderer matPlayer;
    public float speed = 10f;
    public float powerupSpeed = 10.0f;
    public GameObject powerupIndicator;

    bool hasPowerup = false;

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
        rbPlayer.AddForce(focalPoint.transform.forward * magnitude, ForceMode.Force);
        
        //This causes the color of the ball to change
        if(forwardInput > 0)
            matPlayer.material.color = new Color(1.0f - forwardInput, 1.0f, 1.0f - forwardInput);
        else
            matPlayer.material.color = new Color(1.0f + forwardInput, 1.0f, 1.0f + forwardInput);
        powerupIndicator.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdown());
            powerupIndicator.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(hasPowerup && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player has collided with " + collision.gameObject + " with powerup set to: " + hasPowerup);
            Rigidbody rbEnemy = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayDir = collision.gameObject.transform.position - transform.position;
            rbEnemy.AddForce(awayDir * powerupSpeed, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(8);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
}
