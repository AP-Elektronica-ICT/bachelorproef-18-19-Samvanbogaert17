using UnityEngine;
using UnityEngine.UI;

public class audioMute : MonoBehaviour {

    private AudioHandler audioHandler;
    public Button btn;
    public RawImage img;
    public Texture muted;
    public Texture unmuted;

    // script used for the mute/unmute audio button
    // first set the image texture to unmute
	void Start () {
        audioHandler = FindObjectOfType<AudioHandler>();
        UpdateIcon();
        btn.onClick.AddListener(PauseMusic);
	}
    // Muting and unmuting the audio and changing the image texture
    public void PauseMusic()
    {
        audioHandler.ToggleSound();
        UpdateIcon();
    }

    public void UpdateIcon()
    {
        audioHandler = FindObjectOfType<AudioHandler>();
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            img.texture = unmuted;
            audioHandler.source.volume = 1;

        }
        else
        {
            img.texture = muted;
            audioHandler.source.volume = 0;
        }
    }
}