using UnityEngine;
using UnityEngine.UI;

public class decrypt : MonoBehaviour {

    public Button btn;

    // script used for the decryption button (titlescreen)
    void Start () {
        btn.onClick.AddListener(Task);
	}
	
    void Task()
    {
        Encryption.DecryptFile(Encryption.path, Encryption.key);
    }
}