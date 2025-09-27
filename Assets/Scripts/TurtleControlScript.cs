using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TurtleControlScript : MonoBehaviour
{
    public TurtleAnimationScript[] _turtles;
    public Collider2D _killVolume;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _killVolume.enabled = false;
        StartCoroutine(TurtleDiveSeq());
    }

    IEnumerator TurtleDiveSeq()
    {
        float _timer = UnityEngine.Random.Range(5f, 8f);

        yield return new WaitForSeconds(_timer);
        
        foreach(TurtleAnimationScript t in  _turtles)
        {
            t.TurtleDive();
        }
        yield return new WaitForSeconds(2.75f);
        foreach (TurtleAnimationScript t in _turtles)
        {
            t.TurtleRise();
        }
        yield return new WaitForSeconds(2.75f);
        StartCoroutine(TurtleDiveSeq());
    }
}
