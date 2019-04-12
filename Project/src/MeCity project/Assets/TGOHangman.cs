using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TGOHangman : MonoBehaviour
{
    public Text answerTxt;

    public Button submitWord;
    public Button submitLetter;
    public Button resetBtn;

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
    private List<GameObject> letterGOList = new List<GameObject>();

    public Image[] hangman;
    private int count = 0;

    private System.Random rnd = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        wordGridTransform = wordGrid.transform;
        letterGridTransform = letterGrid.transform;

        submitWord.onClick.AddListener(GuessWord);
        submitLetter.onClick.AddListener(GuessLetter);
        resetBtn.onClick.AddListener(StartGame);

        StartGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(wordField.text != "")
            {
                GuessWord();
            }
            else if(letterField.text != "")
            {
                GuessLetter();
            }
        }
    }

    void StartGame()
    {
        answerTxt.text = "";
        answerTxt.color = new Color(0, 0, 0);
        //clear charArray
        charArray = new char[0];
        //clear letterGoList & letters
        letterGOList.Clear();
        letters.Clear();
        //pick random letter
        word = words[rnd.Next(words.Count)];
        //sets word to lowercase
        word = word.ToLower();

        charArray = word.ToCharArray();
        //Clears the word grid
        foreach (Transform transform in wordGridTransform)
        {
            Destroy(transform.gameObject);
        }
        //Clears the letter grid
        foreach (Transform transform in letterGridTransform)
        {
            Destroy(transform.gameObject);
        }
        //Refills the grid with individual words
        for (int i = 0; i < charArray.Length; i++)
        {
            letterGOList.Add(Instantiate(txtPrefab, wordGridTransform));
            if (char.IsWhiteSpace(charArray[i]))
            {
                letterGOList[i].GetComponent<Text>().text = " ";
            }
        }
        //// All code for resetting images
        //disables sad smiley
        SetSadSmiley(0);
        //sets count back to zero
        count = 0;
        //disables all hangman images
        for (int i = 0; i < hangman.Length; i++)
        {
            hangman[i].enabled = false;
        }
    }

    //Method for guessing a letter
    void GuessLetter()
    {
        char letter = '0';
        if (letterField.text.Length == 1)
        {
            letter = char.Parse(letterField.text);
        }
        //reads the letter input field
        //checks if input is a letter
        if (char.IsLetter(letter))
        {
            //sets char to lowercase
            letter = char.ToLower(letter);
            //checks if word contains the given input
            if (charArray.Contains(letter))
            {
                //checks every letter of the word to check the position(s) of the letter
                for (int i = 0; i < charArray.Length; i++)
                {
                    if (charArray[i] == letter)
                    {
                        //replace blank spot at the position of the letter 
                        letterGOList[i].GetComponent<Text>().text = letter.ToString();
                    }
                }
            }
            //if word doesn't contain given input, add letter to the faulty letters grid
            else if(!letters.Contains(letter))
            {
                letters.Add(letter);
                GameObject letterGO = Instantiate(txtPrefab, letterGridTransform);
                letterGO.GetComponent<Text>().text = letter.ToString();

                hangman[count].enabled = true;
                count++;
                if(count == hangman.Length)
                {
                    SetSadSmiley(1);
                    answerTxt.text = "You lost!";
                    answerTxt.color = new Color(1, 0, 0);
                }
            }
        }
        
        //clears inputfield
        letterField.text = "";
        letterField.ActivateInputField();
    }

    //Method for guessing a word
    void GuessWord()
    {
        //reads the word input
        string _word = wordField.text;
        _word = _word.ToLower();
        //word is guessed correctly
        if(_word == word)
        {
            answerTxt.text = "You won!";
            answerTxt.color = new Color(0, 1, 0);
        }
        //word is guessed incorrectly
        else
        {
            answerTxt.text = "You lost!";
            answerTxt.color = new Color(1, 0, 0);

            //enables all hangman images
            for (int i = 0; i < hangman.Length; i++){
                hangman[i].enabled = true;
            }
            //enables sad smiley
            SetSadSmiley(1);
        }

        //replace all blank spots with letter
        for (int i = 0; i < charArray.Length; i++)
        {
            letterGOList[i].GetComponent<Text>().text = charArray[i].ToString();
        }

        //clears inputfield
        wordField.text = "";
    }

    void SetSadSmiley(int mode)
    {
        switch (mode)
        {
            case 0:
                //disables the images that make up the sad smiley face
                for (int i = 0; i < hangman.Length; i++)
                {
                    if (hangman[i].transform.childCount > 0)
                    {
                        foreach (Transform child in hangman[i].transform)
                        {
                            child.GetComponent<Image>().enabled = false;
                        }
                    }
                }
                break;
            case 1:
                //enables the images that make up the sad smiley face
                for (int i = 0; i < hangman.Length; i++)
                {
                    if (hangman[i].transform.childCount > 0)
                    {
                        foreach (Transform child in hangman[i].transform)
                        {
                            child.GetComponent<Image>().enabled = true;
                        }
                    }
                }
                break;
        }
    }
}
