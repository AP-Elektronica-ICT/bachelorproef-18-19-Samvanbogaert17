using UnityEngine;
using UnityEngine.UI;

public class audioMute : MonoBehaviour {

    public Button btn;
    public AudioSource audio;
    public RawImage img;
    public Texture mute;
    public Texture unmute;
    private bool isMuted = false;

    // script used for the mute/unmute audio button
    // first set the image texture to unmute
	void Start () {
        btn.onClick.AddListener(Task);
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
}