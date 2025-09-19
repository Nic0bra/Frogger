using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject playerPrefab;
    public Transform startPoint;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            startPoint = GameObject.FindGameObjectWithTag("StartPoint").transform;
        }
    }

    public void SpawnFrog()
    {
        Instantiate(playerPrefab, startPoint.position, startPoint.rotation);
    }
}
