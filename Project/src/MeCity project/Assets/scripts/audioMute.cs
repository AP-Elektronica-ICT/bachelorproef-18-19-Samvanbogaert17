using UnityEngine;
using UnityEngine.UI;

public class audioMute : MonoBehaviour {

    private AudioHandler audioHandler;
    public Button btn;
    public AudioSource audio;
    public RawImage img;
    public Texture mute;
    public Texture unmute;
    private static bool isMuted = false;

    // script used for the mute/unmute audio button
    // first set the image texture to unmute
	void Start () {
        audioHandler = FindObjectOfType<AudioHandler>();
        isMuted = false;
        btn.onClick.AddListener(PauseMusic);
        img.texture = unmute;
	}
    // Muting and unmuting the audio and changing the image texture
	public void Task()
    {
        if (!isMuted)
        {
            audio.mute = true;
            img.texture = mute;
            isMuted = true;
        } else
        {
            audio.mute = false;
            img.texture = unmute;
            isMuted = false;
        }
    }

    public void PauseMusic()
    {
        audioHandler.ToggleSound();
        UpdateIcon();
    }

    void UpdateIcon()
    {
        if(PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            audio.volume = 1;
            img.texture = unmute;
        }
        else
        {
            audio.volume = 0;
            img.texture = mute;
        }
    }
}