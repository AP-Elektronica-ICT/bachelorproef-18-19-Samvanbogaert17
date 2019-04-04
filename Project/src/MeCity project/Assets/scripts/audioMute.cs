using UnityEngine;
using UnityEngine.UI;

public class audioMute : MonoBehaviour {

    private AudioHandler audioHandler;
    public Button btn;
    public RawImage img;
    public Texture mute;
    public Texture unmute;

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

    void UpdateIcon()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            img.texture = unmute;
        }
        else
        {
            img.texture = mute;
        }
    }
}