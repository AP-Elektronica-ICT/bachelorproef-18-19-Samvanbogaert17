using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TGOMemoryController : MonoBehaviour
{
    public Texture meganDefault;
    public Texture[] imgArray;
    public GameObject meganPrefab;
    public GameObject memoryGrid;

    private Transform memoryGridTransform;

    private List<GameObject> prefabList = new List<GameObject>();
    private List<Texture> answerList = new List<Texture>();

    private List<int> answersPicked = new List<int>();
    private int ansCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        memoryGridTransform = memoryGrid.transform;
        Init();
    }

    private void Init()
    {
        int ind = 0;

        for (int i = 0; i < imgArray.Length * 2; i++)
        {
            int temp = i;

            prefabList.Add(Instantiate(meganPrefab, memoryGridTransform));
            prefabList[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                OnClick(temp);
            });

            //Increases the imgArray index by 1 everytime i can be divided by 2
            //Hereby adding the same images 2 times before adding the next image

            if (i % 2 == 0 && i != 0)
            {
                ind++;
            }

            answerList.Add(imgArray[ind]);
        }

        ShuffleClass.Shuffle(answerList);
    }

    /*
    private void OnClick(int index)
    {
        //checks if the user picked two answers and the answers that the user picked don't form a pair
        if (ansCount == 2 && (answerList[answersPicked[0]] != answerList[answersPicked[1]] || answersPicked[0] == answersPicked[1]))
        {
            //reset images back to the default image
            prefabList[answersPicked[0]].GetComponent<RawImage>().texture = meganDefault;
            prefabList[answersPicked[1]].GetComponent<RawImage>().texture = meganDefault;
            //clear the answers picked list
            answersPicked.Clear();
            ansCount = 0;
        }
        prefabList[index].GetComponent<RawImage>().texture = answerList[index];
        answersPicked.Add(index);

        ansCount++;

        if (ansCount == 2)
        {
            if(answersPicked[0] == answersPicked[1])
            {
                answersPicked.Clear();
                ansCount = 0;
            }
            else if(answerList[answersPicked[0]] == answerList[answersPicked[1]])
            {
                DataScript.AddScore(1000);
                prefabList[answersPicked[0]].GetComponent<Button>().interactable = false;
                prefabList[answersPicked[1]].GetComponent<Button>().interactable = false;
                answersPicked.Clear();
                ansCount = 0;
            }
        }        
    }*/

    private void OnClick(int index)
    {
        if (answersPicked.Count == 2)
        {
            //reset images back to the default image
            prefabList[answersPicked[0]].GetComponent<RawImage>().texture = meganDefault;
            prefabList[answersPicked[1]].GetComponent<RawImage>().texture = meganDefault;
            //clear the answers picked list
            answersPicked.Clear();
        }

        prefabList[index].GetComponent<RawImage>().texture = answerList[index];
        answersPicked.Add(index);

        if(answersPicked.Count == 2)
        {
            if (answersPicked[0] != answersPicked[1])
            {
                if (answerList[answersPicked[0]] == answerList[answersPicked[1]])
                {
                    DataScript.AddScore(1000);
                    prefabList[answersPicked[0]].GetComponent<Button>().interactable = false;
                    prefabList[answersPicked[1]].GetComponent<Button>().interactable = false;
                    answersPicked.Clear();
                }
            }
            else
            {
                //reset images back to the default image
                prefabList[answersPicked[0]].GetComponent<RawImage>().texture = meganDefault;
                prefabList[answersPicked[1]].GetComponent<RawImage>().texture = meganDefault;
                //clear the answers picked list
                answersPicked.Clear();
            }
        }
        
    }

    public void StartGame()
    {
        ansCount = 0;
        answersPicked.Clear();

        for (int i = 0; i < prefabList.Count; i++)
        {
            prefabList[i].GetComponent<RawImage>().texture = meganDefault;
            prefabList[i].GetComponent<Button>().interactable = true;
        }

        ShuffleClass.Shuffle(answerList);
    }
}

public static class ShuffleClass
{
    private static System.Random rnd = new System.Random();

    //Fisher-Yates shuffle method - used to shuffle a list
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
