using UnityEngine;
using System;
using System.Collections;
using UnityEditorInternal;

public class MoveCycleScript : MonoBehaviour
{
    //Control the speed and direction
    public float moveSpeed = 1f;
    public Vector2 moveDirection = Vector2.right;

    //Control the object transition to other side of the screen
    public int objSize = 1;
    [SerializeField] Vector2 rightEdge;
    [SerializeField] Vector2 leftEdge;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector2.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector2.right);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveDirection.x > 0 && (transform.position.x - objSize) > rightEdge.x)
        {
            Vector2 _position = transform.position;
            _position.x = leftEdge.x - objSize;
            transform.position = _position;
        }

        else if(moveDirection.x < 0 && (transform.position.x + objSize) < leftEdge.x)
        {
            Vector2 _position = transform.position;
            _position.x = rightEdge.x + objSize;
            transform.position = _position;
        }
        else
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
