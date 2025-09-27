using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public FrogAttemptTimer attemptTimer;
    public FrogController currentFrog;
    public static GameManager Instance;
    public GameObject playerPrefab;
    public Transform startPoint;

    public int lifeCounter = 5;
    public int lilypadCounter = 5;
    public TMP_Text lifeText;

    /// Manage End of Game Screens
    public GameObject winScreen;
    public GameObject loseScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundManager.Instance.PlayMusic("Background", true);
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            startPoint = GameObject.FindGameObjectWithTag("StartPoint").transform;
        }

        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        SpawnFrog();
    }

    private void Update()
    {
        lifeText.text = lifeCounter.ToString();
    }
    public void SpawnFrog()
    {
        if (lifeCounter > 0 && lilypadCounter > 0)
        {
            if(currentFrog != null)
            {
                Debug.Log("Tried to spawn while a frog still exists. Aborting spawn");
                return;
            }
            var frogGo = Instantiate(playerPrefab, startPoint.position, startPoint.rotation);
            var frog = frogGo.GetComponent<FrogController>();
            currentFrog = frog;

            Debug.Log("Spawned Frog");

            if (attemptTimer != null && frog != null)
            {
                attemptTimer.BeginTimer(frog);
            }
        }
        else
        {
            if(lifeCounter <= 0)
            {
                loseScreen.SetActive(true);
            }
                
                
            else if(lilypadCounter <= 0)
            {
                winScreen.SetActive(true);
            }

            StartCoroutine(RestartGame());
        }
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(0);
    }
}
