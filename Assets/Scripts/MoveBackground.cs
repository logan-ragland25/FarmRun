using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    public float speed = -5;
    public GameManager manager;
    public double restartDistance;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        //repeatWidth = GetComponent<BoxCollider>().size.z / 2;
        //Debug.Log(repeatWidth);
    }

    private void Update()
    {
        if (manager.isGameActive == true)
        {
            Move();
        }
    }

        // Update is called once per frame
    void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed * 2);
        
        if (transform.position.z < startPos.z - restartDistance)
        {
            transform.position = startPos;
        }
    }
}
