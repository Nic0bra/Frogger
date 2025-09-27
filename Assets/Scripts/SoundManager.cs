using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;

    public void PlayMusic(string groupId= "Background", bool loop = true)
    {
        if (musicSource == null)
        {
            return;
        }
        var clip = sfxLibrary.GetClipFromName(groupId);
        if (clip == null)
        {
            return;
        }

        musicSource.loop = loop;
        if(musicSource.clip == clip && musicSource.isPlaying )
        {
            return;
        }
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource == null)
        {
            return;
        }
        musicSource.Stop();
    }
    //Call from any where in the game
    public static SoundManager Instance;
    [SerializeField] private SoundLibrary sfxLibrary;

    public AudioSource sfxSource;
    //Call before game is loaded
    private void Awake()
    {
        if (Instance != null)
        {
            //if one already exists then it destroys itself
            Destroy(gameObject);
        }
        //otherwise this becomes the instance
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// Plays a generic 3D sound
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="position"></param>
    public void PlaySound3D(AudioClip clip, Vector3 position)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position);
        }
    }
    /// <summary>
    /// Plays a 3D sound from the sound library
    /// </summary>
    /// <param name="soundName"></param>
    /// <param name="position"></param>
    public void PlaySound3d(string soundName, Vector3 position)
    {
        PlaySound3D(sfxLibrary.GetClipFromName(soundName), position);
    }
    //Calls a sound period
    public void PlaySound2D(string soundName)
    {
        sfxSource.PlayOneShot(sfxLibrary.GetClipFromName(soundName));
    }
}