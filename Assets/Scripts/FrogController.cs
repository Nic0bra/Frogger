using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

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
        if (!isDead)
        {

            if (_actions.Player.Up.triggered)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
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
    }

    void PlayerMove(Vector2 _direction)
    {
        if (isDead)
        {
            return;
        }
        canMove = false;
        _anim.SetTrigger("Hop");
        Vector2 _destination = transform.position + (Vector3)_direction;

        Collider2D _barrier = Physics2D.OverlapBox(_destination,
                                                    new Vector2(.9f, .9f),
                                                    0,
                                                    LayerMask.GetMask("Barrier"));
        if (_barrier!= null)
        {
            canMove = true;
            return;
        }
        SoundManager.Instance.PlaySound2D("Hop");


        Collider2D _platform = Physics2D.OverlapBox(_destination,
                                                    new Vector2(.9f, .9f),
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
            if (isDead)
            {
                yield break;
            }
            float _time = elapsedTime / duration;
            transform.position = Vector2.Lerp(startPos, _destination, _time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = _destination;
        yield return new WaitForSeconds(moveTime);
        if (isDead)
        {
            yield break;
        }
        if (onRiver && !onPlatform)
        {
            KillPlayer();
            yield break;
        }

        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead)
        {
            return;
        }
        if (other.gameObject.tag == "Obstacle")
        {
            SoundManager.Instance.PlaySound2D("Squash");
            KillPlayer();
        }
        if (other.gameObject.tag == "River")
        {
            SoundManager.Instance.PlaySound2D("Plunk");
            onRiver = true;
        }
        if(other.gameObject.tag == "Kill")
        {
            SoundManager.Instance.PlaySound2D("Squash");
            KillPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "River")
        {
            onRiver = false;
        }
    }

    public void KillPlayer()
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
        canMove = false;
        StopAllCoroutines();
        StartCoroutine(FrogDeath());
    }
    IEnumerator FrogDeath()
    {
        transform.SetParent(null);
        onPlatform = false;

        var col = GetComponent<Collider2D>();
        if(col)
        {
            col.enabled = false;
        }

        if(GameManager.Instance && GameManager.Instance.attemptTimer)
        {
            GameManager.Instance.attemptTimer.OnFrogDied();
        }
        _anim.SetTrigger("Dead");
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.lifeCounter--;
        if(GameManager.Instance.currentFrog == this)
        {
            GameManager.Instance.currentFrog = null;
        }
        Destroy(gameObject);
        GameManager.Instance.SpawnFrog();
    }
}
