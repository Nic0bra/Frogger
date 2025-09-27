using System;
using System.Collections;
using UnityEngine;

public class LilyPadScript : MonoBehaviour
{
    public GameObject frogImage;
    public bool isOccupied;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        frogImage.SetActive(false);
        isOccupied = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOccupied)
        {
            if (other.gameObject.tag == "Player")
            {
                isOccupied = true;
                frogImage.SetActive (true);
                GameManager.Instance.lilypadCounter--;

                if(GameManager.Instance.attemptTimer != null)
                {
                    GameManager.Instance.attemptTimer.OnReachedLilyPad();
                }

                if(GameManager.Instance.currentFrog != null)
                {
                    GameManager.Instance.currentFrog = null;
                }
                GameManager.Instance.SpawnFrog();
                Destroy(other.gameObject);
            }
            else
            {
                FrogController controller = other.gameObject.GetComponent<FrogController>();
                controller.KillPlayer();
            }
        }
        else
        {
            FrogController controller = other.gameObject.GetComponent<FrogController>();
            controller.KillPlayer();
        }

    }
}
