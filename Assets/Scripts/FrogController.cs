using UnityEngine;
using System;
using System.Collections;

public class FrogController : MonoBehaviour
{
    Frogger_InputActions _actions;

    [SerializeField] Animator _anim;
    public float moveTime = 0.15f;
    [SerializeField] private bool canMove;
    [SerializeField] private bool isDead;
    [SerializeField] private bool onRiver;
    [SerializeField] private bool onPlatform;

    private void Awake()
    {
        _actions = new Frogger_InputActions();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canMove = true;
        _anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        _actions.Enable();
    }

    private void OnDisable()
    {
        _actions.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        if(canMove) PlayerUpdate();
    }

    void PlayerUpdate()
    {
        if (_actions.Player.Up.triggered)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            PlayerMove(Vector2.up);
        }
        if (_actions.Player.Down.triggered)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            PlayerMove(Vector2.down);
        }
        if (_actions.Player.Left.triggered)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            PlayerMove(Vector2.left);
        }
        if (_actions.Player.Right.triggered)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            PlayerMove(Vector2.right);
        }
    }

    void PlayerMove(Vector2 _direction)
    {
        canMove = false;
        _anim.SetTrigger("Hop");
        Vector2 _destination = transform.position + (Vector3)_direction;

        Collider2D _platform = Physics2D.OverlapBox(_destination, 
                                                    Vector2.zero, 
                                                    0, 
                                                    LayerMask.GetMask("Platform"));
        if (_platform != null)
        {
            transform.SetParent(_platform.transform);
            onPlatform = true;
        }
        else
        {
            transform.SetParent(null);
            onPlatform = false;
        }

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
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = _destination;
        yield return new WaitForSeconds(moveTime);
        canMove = true;
    }
}
