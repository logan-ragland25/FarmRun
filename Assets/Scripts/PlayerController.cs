using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalSpeed = 100;
    public float verticalSpeed = 50;
    public bool gameOver = false;
    public float jumpForce = 50;
    private bool onGround;
    public float topBound = 21;
    public float bottomBound = -2;
    public float leftBound = -16;
    public float rightBound = 16;
    public float yPhysics = 10;
    public float zPhysics = 10;
    public float pushStrength = 20;
    public float horsePushStrength = 100;
    public float maxRightRotation = 45;
    public float maxLeftRotation = -45;
    public float yRotate = 0;
    public float rotateSpeed = 550;
    public float maxHeight;
    public GameManager manager;
    [SerializeField] float changeSizeDelay;
    bool getFlung = true;
    bool canGrow = true;
    bool started = false;

    [SerializeField] AudioSource boomSound;
    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void StartGame()
    {
        leftBound = -16;
        rightBound = 16;
        if(manager.level == 3)
        {
            leftBound = -12;
            rightBound = 12;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        ConstrainPlayerPosition();
        if (manager.isGameActive == true && started == false)
        {
            StartGame();
        }
        else if (manager.isGameActive == false)
        {
            started = false;
        }

    }
    void FixedUpdate()
    {
        if (manager.isGameActive == true)
        {
            MovePlayer();
            yRotate += Time.deltaTime * Input.GetAxis("Horizontal") * rotateSpeed;
            yRotate = Mathf.Clamp(yRotate, -45, 45);
            transform.eulerAngles = new(0, yRotate, 0);
        }
    }
    //Moves player based on key input
    void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        playerRb.AddForce(Vector3.forward * verticalSpeed * verticalInput);
        playerRb.AddForce(Vector3.right * horizontalSpeed * horizontalInput);
        playerRb.transform.Rotate(Vector3.up * horizontalInput * rotateSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && gameOver != true && onGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce);
            onGround = false;
        }

    }

    //Prevent player from leaving bounds
    void ConstrainPlayerPosition()
    {
        //Boundries
        if (transform.position.z < bottomBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, bottomBound);
        }

        if (transform.position.z > topBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, topBound);
        }

        if (transform.position.x < leftBound)
        {
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
        }

        if (transform.position.x > rightBound)
        {
            transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
        }
        if (transform.position.y > maxHeight)
        {
            playerRb.velocity = new Vector3 (0, -5, 0);
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
            
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.name != "enemy2" && other.gameObject.name != "enemy3")
        {
            //other.rigidbody.AddForce(Vector3.up * speed * yPhysics);
            //other.rigidbody.AddForce(Vector3.forward * speed * zPhysics);
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * pushStrength, ForceMode.Impulse);
            enemyRigidbody.AddForce(Vector3.up * 100, ForceMode.Impulse);
            boomSound.Play();
        }

        if (other.gameObject.name == "enemy0")
        {
            manager.UpdateScore(50);
        }

        if (other.gameObject.name == "enemy1")
        {
            manager.UpdateScore(100);
        }

        if (other.gameObject.name == "enemy2")
        {
            //other.rigidbody.AddForce(Vector3.up * speed * yPhysics);
            //other.rigidbody.AddForce(Vector3.forward * speed * zPhysics);
            if (getFlung == true)
            {
                Vector3 awayFromHorse = -other.gameObject.transform.position;

                playerRb.AddForce(Vector3.up * 1500, ForceMode.Impulse);
                playerRb.AddForce(awayFromHorse * horsePushStrength, ForceMode.Impulse);
            }
            manager.UpdateHorse();
            manager.UpdateScore(250);
            boomSound.Play();
        }

        if (other.gameObject.name == "enemy3")
        {
            if (getFlung == true)
            {
                Vector3 awayFromHorse = -other.gameObject.transform.position;

                playerRb.AddForce(Vector3.up * 1500, ForceMode.Impulse);
                playerRb.AddForce(awayFromHorse * horsePushStrength, ForceMode.Impulse);
                manager.SubtractLives(1);
            }

            boomSound.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Powerup"))
        {
            if (other.gameObject.name == "powerup0")
            {
                manager.AddLives(1);
                manager.UpdateScore(50);
            }
            else if (other.gameObject.name == "powerup1" && canGrow == true)
            {
                StartCoroutine(ChangeSize());
                manager.UpdateScore(50);
            }
            Destroy(other.gameObject);
        } 
    }

    private IEnumerator ChangeSize()
    {
        Vector3 size = playerRb.transform.localScale;
        playerRb.transform.localScale = new Vector3 (size.x * 3, size.y * 3, size.z * 3);
        horizontalSpeed *= 2;
        verticalSpeed *= 2;
        getFlung = false;
        pushStrength *= 2;
        canGrow = false;

        yield return new WaitForSeconds(changeSizeDelay);

        playerRb.transform.localScale = new Vector3(size.x, size.y, size.z);
        horizontalSpeed /= 2;
        verticalSpeed /= 2;
        getFlung = true;
        pushStrength /= 2;
        canGrow = true;
    }
}
