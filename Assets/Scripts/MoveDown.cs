using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveDown : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private int zBound = -10;
    public bool move = true;
    public float moveHeight = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < moveHeight)
        {
            move = true;
        }
        else
        {
            move = false;
        }
        if (move)
        {
            rb.AddForce(Vector3.forward * -speed);
        }
        if (transform.position.z < zBound)
        {
            Destroy(gameObject);
        }
    }

    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            move = true;
        }
        else
        {
            move = false;
        }
    }*/
}
