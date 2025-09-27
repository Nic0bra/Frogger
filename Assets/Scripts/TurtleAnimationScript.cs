using UnityEngine;

public class TurtleAnimationScript : MonoBehaviour
{
    [SerializeField] TurtleControlScript turtleCTRL;
    [SerializeField] Animator _Anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turtleCTRL = GetComponentInParent<TurtleControlScript>();
        _Anim = GetComponent<Animator>();
    }

    public void TurtleDive()
    {
        _Anim.SetTrigger("Dive");
    }

    public void TurtleRise()
    {
        _Anim.SetTrigger("Rise");
    }

    public void ColliderEnable()
    {
        turtleCTRL._killVolume.enabled = true;
    }
    public void ColliderDisable()
    {
        turtleCTRL._killVolume.enabled = false;
    }
}
