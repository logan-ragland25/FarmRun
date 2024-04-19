using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject endLocation;
    public bool isHorse = false;
    public float pushStrength = -250;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        endLocation = GameObject.Find("EndLocation");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (endLocation.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            enemyRb.freezeRotation = true;
        }
        else
        {
            enemyRb.freezeRotation = false;
        }
        /*if (other.gameObject.CompareTag("Player") && isHorse == true)
        {
            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position;
            playerRigidbody.AddForce(awayFromPlayer * pushStrength, ForceMode.Impulse);
            playerRigidbody.AddForce(Vector3.up * 100, ForceMode.Impulse);
            Debug.Log("TRUE");
        }*/
    }
}
