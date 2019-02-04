using UnityEngine;
using UnityEngine.UI;

public class encrypt : MonoBehaviour {

    public Button btn;

    // script used for the encryption button (titlescreen)
    void Start () {
        btn.onClick.AddListener(Task);
	}
	void Task()
    {
        Encryption.EncryptFile(Encryption.path, Encryption.key);
    }
}