using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioHandler : MonoBehaviour
{
    static AudioHandler instance = null;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip levelMusic;
    [SerializeField] private AudioClip introMusic;
    public AudioSource source;
    private string scene;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //Makes this object persist between scenes
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if(scene != SceneManager.GetActiveScene().name)
        {
            scene = SceneManager.GetActiveScene().name;
            //checks if the scene is a lever or not
            //add new level names to this list
            if (scene.IsOneOf("Producer","TGO","DGO","Supplier","Consumer"))
            {
                PlayLevelMusic();
            }
            else if (scene.IsOneOf("Introduction"))
            {
                PlayIntroductionMusic();
            }
            else
            {
                PlayMenuMusic();
            }
        }
    }

    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
            
        }
    }

    public void PlayMenuMusicStatic()
    {
        if(instance != null)
        {
            if(instance.source != null)
            {
                instance.source.Stop();
                instance.source.clip = instance.menuMusic;
                instance.source.Play();
            }
        }
    }

    //wrapper method for PlayMenuMusicStatic
    public void PlayMenuMusic()
    {
        PlayMenuMusicStatic();
    }

    public void PlayLevelMusicStatic()
    {
        if (instance != null)
        {
            if (instance.source != null)
            {
                instance.source.Stop();
                instance.source.clip = instance.levelMusic;
                instance.source.Play();
            }
        }
    }

    //wrapper method for PlayLevelMusicStatic
    public void PlayLevelMusic()
    {
        PlayLevelMusicStatic();
    } 

    public static void PlayIntroductionMusicStatic()
    {
        if (instance != null)
        {
            if (instance.source != null)
            {
                instance.source.Stop();
                instance.source.clip = instance.introMusic;
                instance.source.Play();
            }
        }
    }

    //wrapper method for PlayIntroductionMusicStatic
    public void PlayIntroductionMusic()
    {
        PlayIntroductionMusicStatic();
    }
}

public static class Helper
{
    public static bool IsOneOf<T>(this T value, params T[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Equals(value))
            {
                return true;
            }
        }

        return false;
    }
}


