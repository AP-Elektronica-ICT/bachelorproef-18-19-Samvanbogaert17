using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Admin : MonoBehaviour
{
    public Button adminBtn;
    public InputField passwordInput;

    private static string GetSHA512(string String)
    {
        UnicodeEncoding ue = new UnicodeEncoding();
        byte[] hashValue;
        byte[] message = ue.GetBytes(String);
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";

        hashValue = hashString.ComputeHash(message);

        foreach(byte x in hashValue)
        {
            hex += string.Format("{0:x2}", x);
        }
        return hex;
    }

    // Start is called before the first frame update
    void Start()
    {
        adminBtn.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (passwordInput.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            if(passwordInput.text == )
        }*/
    }


}
