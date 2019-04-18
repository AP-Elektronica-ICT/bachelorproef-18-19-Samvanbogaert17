using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGOCatcher : MonoBehaviour
{
    public GameObject lightningPrefab;
    public GameObject bucket;

    public GameObject gamePanel;
    private Transform gamePanelTransform;

    public Text livesTxt;
    public Text gameTxt;

    public float baseLightningSpeed = 150f;
    [HideInInspector] public static float lightningSpeed;
    public static int lives = 3;
    public static bool playerIsReady = false;

    private System.Random rnd = new System.Random();
    private int timer;

    private int time;
    private int simultaniousInstances;
    private float speedOffset;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        gamePanelTransform = gamePanel.transform;

        ResetGame();

        bucket.GetComponent<Button>().onClick.AddListener(IsPressed);
    }

    // Update is called once per frame
    void Update()
    {
        if (TGOMinigamesController.GameStarted && GetComponent<Canvas>().enabled)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var child = gameObject.transform.GetChild(i).gameObject;
                if (child != null)
                {
                    child.SetActive(true);
                }
            }

            if (playerIsReady)
            {
                //periodically instantiate lightning objects
                if (timer == time) // 60 frames or 1 second have passed
                {
                    for (int i = 0; i < simultaniousInstances; i++)
                    {
                        GetRandomSpeedOffset();
                        GetRandomAngle();
                        GameObject _gameObject;
                        _gameObject = Instantiate(lightningPrefab, gamePanelTransform);
                        _gameObject.transform.localPosition = new Vector2(rnd.Next(50, 550), -20);
                        _gameObject.transform.Rotate(new Vector3(0, 0, angle));
                        _gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2((-lightningSpeed + speedOffset) * Mathf.Cos(angle), (-lightningSpeed + speedOffset) * Mathf.Sin(angle));

                    }
                    GetRandomTime();
                    GetRandomSimultaniousInstances();
                    timer = 0;
                    Debug.Log(lightningSpeed);
                }
                CheckLives();
                timer++;
            }
        }
        else
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var child = gameObject.transform.GetChild(i).gameObject;
                if (child != null)
                {
                    child.SetActive(false);
                }
            }
        }

    }

    void Setup()
    {
        playerIsReady = false;
        gameTxt.enabled = true;
        bucket.transform.localPosition = new Vector2(288, -380);
    }

    public void ResetGame()
    {
        GetRandomValues();
        lives = 3;
        livesTxt.text = lives.ToString();
        lightningSpeed = baseLightningSpeed;
        Setup();
    }

    public void CheckLives()
    {
        if (lives < 0)
        {
            ResetGame();
            DataScript.AddScore(-2500);
        }
        livesTxt.text = lives.ToString();
    }
    void IsPressed()
    {
        playerIsReady = true;
        Debug.Log(playerIsReady);
        gameTxt.enabled = false;
    }

    void GetRandomAngle()
    {
        angle = rnd.Next(80, 100);
        angle *= Mathf.PI / 180;
    }

    void GetRandomValues()
    {
        GetRandomSpeedOffset();
        GetRandomSimultaniousInstances();
        GetRandomTime();
        GetRandomAngle();
    }

    void GetRandomSpeedOffset()
    {
        speedOffset = rnd.Next(-50, 50);
    }

    void GetRandomSimultaniousInstances()
    {
        simultaniousInstances = rnd.Next(1, 4);
    }

    void GetRandomTime()
    {
        time = rnd.Next(30, 90);
    }


}
