using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rbEnemy;
    GameObject player;
    public float speed = 5f;

    void Start()
    {
        rbEnemy = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 seekDirection = (player.transform.position - transform.position).normalized;
        rbEnemy.AddForce(seekDirection * speed * Time.deltaTime);
    }
}
