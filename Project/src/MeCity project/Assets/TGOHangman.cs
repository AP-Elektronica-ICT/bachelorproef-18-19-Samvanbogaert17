using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TGOHangman : MonoBehaviour
{
    public InputField letterField;
    public InputField wordField;

    public GameObject wordGrid;
    private Transform wordGridTransform;

    public GameObject letterGrid;
    private Transform letterGridTransform;

    public GameObject txtPrefab;
   
    public List<string> words = new List<string>();
    private string word;
    private char[] charArray;

    private List<char> letters = new List<char>();
    private List<GameObject> letterGO = new List<GameObject>();

    private System.Random rnd = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        wordGridTransform = wordGrid.transform;
    }

    void Update()
    {
        
    }

    void StartGame()
    {
        //clear charArray
        charArray = new char[0];
        word = words[rnd.Next(words.Count)];
        charArray = word.ToCharArray();
        //Clears the grid
        foreach(Transform transform in wordGridTransform)
        {
            Destroy(transform);
        }
        //Refills the grid with individual words
        for(int i = 0; i < charArray.Length; i++)
        {
            Instantiate(txtPrefab, wordGridTransform);
        }
    }

    void GuessLetter()
    {
        char letter = char.Parse(letterField.text);
        if (charArray.Contains(letter))
        {
            for (int i = 0; i < charArray.Length; i++)
            {
                if(charArray[i] == letter)
                {

                }
            }
        }
        else
        {
            GameObject letterGO = Instantiate(txtPrefab, letterGridTransform);
            letterGO.GetComponent<Text>().text = letter.ToString();
        }
    }
}
