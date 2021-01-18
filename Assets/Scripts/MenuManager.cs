using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void Start()
    {
        //Scores.lastScore = PlayerPrefs.GetInt("lastScore", 0);
        //Scores.bestScore = PlayerPrefs.GetInt("bestScore", 0);

        scoreText.text = "Última puntuación: " + PlayerPrefs.GetInt("lastScore", 0) + 
                        "\nMejor puntuación: " + PlayerPrefs.GetInt("bestScore", 0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
