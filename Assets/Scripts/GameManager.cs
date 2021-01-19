using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Collections;
using System;
using NUnit.Framework.Constraints;

public class GameManager : MonoBehaviour
{
    public static int numberGameTypes = 6;
    private enum GameTypes { TOUCH = 0, SHAKE = 1, UPSIDEDOWN = 2, BLOW = 3, RIGHT = 4, LEFT = 5 };
    private GameTypes gameType;
    private enum GameStates { RUNNING = 0, SUCCESS = 1, FAILURE = 2 };
    private GameStates gameState;
    private int points = 0;

    public GameObject correctUI;
    public GameObject pointsUI;
    public TextMeshProUGUI pointstext;
    public GameObject[] gameTypeUI;
    public AudioClip[] Acciones = new AudioClip[6];
    public AudioClip Victoria, Derrota;

    private AudioSource sonido;

    public TextMeshProUGUI debugText;

    const float minShake = 10;
    const float minDecib = -15;
    const float maxTime = 2;

    private float compassLimitStart;
    private float compassLimitEnd;

    private float timeElapsedSinceGame;

    // Start is called before the first frame update
    void Start()
    {
        pointstext = pointsUI.GetComponent<TextMeshProUGUI>();

        //Input.location.Stop();
        Input.location.Start();

        Input.gyro.enabled = true;
        Input.compass.enabled = true;

        sonido = gameObject.GetComponent<AudioSource>();

        RandomGameType();
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = Input.compass.trueHeading.ToString();

        

        if (gameState == GameStates.RUNNING)
            switch (gameType)
            {
                case GameTypes.TOUCH:
                    if (Input.touchCount > 0)
                        if (Input.GetTouch(0).phase == TouchPhase.Began)
                            GameWon();
                    
                    break;

                case GameTypes.SHAKE:
                    //Debug.Log(Input.acceleration.sqrMagnitude);
                    if (Input.acceleration.sqrMagnitude > minShake)
                        GameWon();
                    break;

                case GameTypes.UPSIDEDOWN:
                    if (Input.gyro.gravity.x < 0.3 && Input.gyro.gravity.x > -0.3 &&
                        Input.gyro.gravity.y < 0.3 && Input.gyro.gravity.z > -0.3 &&
                        Input.gyro.gravity.z > 0.7)
                        GameWon();
                    break;

                case GameTypes.BLOW:
                    //Debug.Log(MicInput.MicLoudnessinDecibels);
                    if (MicInput.MicLoudnessinDecibels > minDecib)
                        GameWon();
                    break;

                case GameTypes.LEFT:
                case GameTypes.RIGHT:
                    if (compassLimitStart > compassLimitEnd)
                    {
                        if (Input.compass.trueHeading >= compassLimitStart || Input.compass.trueHeading <= compassLimitEnd)
                            GameWon();
                    }
                    else
                    {
                        if (Input.compass.trueHeading >= compassLimitStart && Input.compass.trueHeading <= compassLimitEnd)
                            GameWon();
                    }
                    break;
                    
            }
        
        timeElapsedSinceGame += Time.deltaTime;

        if(timeElapsedSinceGame >= maxTime)
        {
            if (gameState == GameStates.SUCCESS)
                RandomGameType();
            else
            {
                EndGame();
            }
        }
    }

    public void RandomGameType()
    {
        gameType = (GameTypes)UnityEngine.Random.Range(0, numberGameTypes);
        gameTypeUI[(int)gameType].SetActive(true);

        if (sonido = null)
        {
            sonido = gameObject.GetComponent<AudioSource>();
        }

        sonido.clip = Acciones[(int)gameType];
        sonido.Play();
        // sonido de cada tipo de juego

        if (gameType == GameTypes.LEFT)
        {
             

            float compassReference = Input.compass.trueHeading;
            compassLimitStart = compassReference - 110;
            if (compassLimitStart < 0) compassLimitStart += 360;
            compassLimitEnd = compassReference - 70;
            if (compassLimitEnd < 0) compassLimitEnd += 360;
        }

        if(gameType == GameTypes.RIGHT)
        {
            float compassReference = Input.compass.trueHeading;
            compassLimitStart = compassReference + 70;
            if (compassLimitStart > 360) compassLimitStart -= 360;
            compassLimitEnd = compassReference + 110;
            if (compassLimitEnd > 360) compassLimitEnd -= 360;
        }

        gameState = GameStates.RUNNING;

        timeElapsedSinceGame = 0;
    }

    public void GameWon()
    {
        gameTypeUI[(int)gameType].SetActive(false);
        gameState = GameStates.SUCCESS;
        points++;
        pointstext.text = "Points: " + points;
        // sonido de acierto
        StartCoroutine(ShowCorrect());
    }

    public IEnumerator ShowCorrect()
    {
        correctUI.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        correctUI.SetActive(false);
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("lastScore", points);
        if (points > PlayerPrefs.GetInt("bestScore", 0))
        {
            sonido.clip = Victoria;
            sonido.Play();
            PlayerPrefs.SetInt("bestScore", points);
        }
        else
        {
            sonido.clip = Derrota;
            sonido.Play();
        }

        Input.location.Stop();

        Input.gyro.enabled = false;
        Input.compass.enabled = false;

        SceneManager.LoadScene("Menu");
    }
}
