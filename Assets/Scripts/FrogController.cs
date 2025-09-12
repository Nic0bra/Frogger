using UnityEngine;
using System;
using System.Collections;

public class FrogController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerUpdate();
    }

    void PlayerUpdate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            PlayerMove(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            PlayerMove(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            PlayerMove(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            PlayerMove(Vector2.right);
        }
    }

    void PlayerMove(Vector2 _direction)
    {
        Vector2 _destination = transform.position + (Vector3)_direction;
        StartCoroutine(LerpMove(_destination));
    }

    IEnumerator LerpMove(Vector2 _destination)
    {
        Vector2 startPos = transform.position;
        float elapsedTime = 0f;
        float duration = .1f;
        
        while (elapsedTime < duration)
        {
            float _time = elapsedTime / duration;
            transform.position = Vector2.Lerp(startPos, _destination, _time);
            elapsedTime -= Time.deltaTime;
            yield return null;
        }

        transform.position = _destination;
    }
}
