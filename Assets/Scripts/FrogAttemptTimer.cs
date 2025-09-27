using UnityEngine;
using UnityEngine.UI;


public class FrogAttemptTimer : MonoBehaviour
{
    //Time variables
    public float timeLimit = 90f;
    private float currentTime;
    private bool running;

    //UI connections
    public Image timerForegroundImage;

    private FrogController activeFrog;

    // Update is called once per frame
    void Update()
    {
        if(!running || activeFrog == null)
        {
            return;
        }

        currentTime -= Time.deltaTime;
        float t = Mathf.Clamp01(currentTime/ timeLimit);
        if(timerForegroundImage != null)
        {
            if (timerForegroundImage) timerForegroundImage.fillAmount = t;
        }
        else
        {
            Debug.Log("No foreground image assigned");
        }


        if (currentTime <= 0)
        {
            Debug.Log("Time ran out - killing frog");
            running = false;
            var dying = activeFrog;
            activeFrog = null;
            if (dying != null)
            {
                dying.KillPlayer();
            }
        }
    }
    public void BeginTimer(FrogController frog)
    {
        activeFrog = frog;
        currentTime = timeLimit;
        running = true;

        if (timerForegroundImage != null)
        {
            if (timerForegroundImage) timerForegroundImage.fillAmount = 1f;
            Debug.Log("Started new timer at full");
        }
    }

    public void OnReachedLilyPad()
    {
        Debug.Log("Frog reached lily pad - timer stopped");
        running = false;
        activeFrog = null;
        if (timerForegroundImage) timerForegroundImage.fillAmount = 1f;
    }

    public void OnFrogDied()
    {
        Debug.Log("Frog died by other - timer stopped");
        running = false;
        activeFrog = null;
        if (timerForegroundImage) timerForegroundImage.fillAmount = 1f;
    }
}
