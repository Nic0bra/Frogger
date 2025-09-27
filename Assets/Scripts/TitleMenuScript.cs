using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuScript : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.StopMusic();
    }
    public string loadThisScene;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        SoundManager.Instance.PlaySound2D("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(loadThisScene);
    }
}
