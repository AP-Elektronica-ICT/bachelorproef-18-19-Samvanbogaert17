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
    private List<GameObject> prefabList = new List<GameObject>();
    private List<Texture> answerList = new List<Texture>();

    private int[] answersPicked = new int[2];
    private int ansCount = 0;
    private bool initialized = false;
    private System.Random rnd = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (initialized)
        {
            if(ansCount == 2)
            {
                if(answersPicked[0] == answersPicked[1])
                {
                    DataScript.AddScore(1000);
                    prefabList[answersPicked[0]].SetActive(false);
                    prefabList[answersPicked[1]].SetActive(false);
                }
                ResetImages();
                answersPicked[0] = 0;
                answersPicked[1] = 0;
                ansCount = 0;
            }
        }  
    }

    private void Init()
    {
        int ind = 0;

        for (int i = 0; i < 18; i++)
        {
            int temp = i;

            prefabList.Add(Instantiate(meganPrefab, transform));
            prefabList[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                OnClick(temp);
            });

            if(i % 2 == 0 && i != 0)
            {
                ind++;
            }

            answerList.Add(imgArray[ind]);
        }

        Randomize();
        initialized = true;
    }

    private void Randomize()
    {
        answerList.OrderBy(x => rnd.Next());
    }

    private void OnClick(int index)
    {
        prefabList[index].GetComponent<RawImage>().texture = answerList[index];
        answersPicked[ansCount] = index;
        ansCount++;
    }

    private void ResetList()
    {
        for(int i = 0; i < prefabList.Count; i++)
        {
            prefabList[i].SetActive(true);
        }
        ResetImages();
    }

    private void ResetImages()
    {
        for (int i = 0; i < prefabList.Count; i++)
        {
            prefabList[i].GetComponent<RawImage>().texture = meganDefault;
        }
    }

    public void StartGame()
    {
        ResetList();
        Randomize();
    }
}
