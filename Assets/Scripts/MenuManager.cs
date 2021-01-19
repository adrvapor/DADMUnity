using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI bestScoreText;


    public void Start()
    {
        //Scores.lastScore = PlayerPrefs.GetInt("lastScore", 0);
        //Scores.bestScore = PlayerPrefs.GetInt("bestScore", 0);

        lastScoreText.text = PlayerPrefs.GetInt("lastScore", 0).ToString();
        bestScoreText.text = PlayerPrefs.GetInt("bestScore", 0).ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
