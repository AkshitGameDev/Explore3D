using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource source;

    public AudioClip shootClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void Play( AudioClip clip )
    {
        source.clip = clip;
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void Mute (bool isMute)
    {
        source.mute = isMute;
    }

}
