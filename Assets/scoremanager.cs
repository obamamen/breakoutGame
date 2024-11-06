using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scoremanager : MonoBehaviour
{
    public int score = 0;
    public int lives = 5;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI liveText;
    public TextMeshProUGUI highScoreText;
    public Transform camera;
    public Button resetButton;


    void Start()
    {
        resetButton.gameObject.SetActive(false);
    }


    void Update()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Brick");

        if (Input.GetKey(KeyCode.P))
        {
            foreach (GameObject obj in objectsWithTag)
            {
                Destroy(obj);
            }
        }
        if (Input.GetKey(KeyCode.O))
        {
            score++;
        }
        if (Input.GetKey(KeyCode.R))
        {
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.Save();
        }



        bool killBall = false;

        if (lives <= 0)
        {

            killBall = true;
            camera.position += Vector3.left / 80;
            if (camera.position.x < -23)
            {
                resetButton.gameObject.SetActive(true);
                camera.position = new Vector3(-23, camera.position.y, camera.position.z);
            }
        }
        else if (objectsWithTag.Length <= 0)
        {
            killBall = true;
            camera.position -= Vector3.left / 80;
            if (camera.position.x > 23)
            {
                resetButton.gameObject.SetActive(true);
                camera.position = new Vector3(23, camera.position.y, camera.position.z);
            }
        }

        if (killBall)
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            if (balls.Length == 1)
            {
                Destroy(balls[0]);
            }
            
        }

        scoreText.text = "score: " + score;
        liveText.text = "lives left: " + lives;
        CheckHighScore();
        LoadHighScore();
    }

    void clickedOnBuyButton()
    {
        if ((lives != 0) && (score >= 250))
        {
            score -= 250;
            lives += 1;
        }
    }

    void playAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void CheckHighScore()
    {
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > savedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score); 
            PlayerPrefs.Save();
        }
    }

    private void LoadHighScore()
    {
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + savedHighScore.ToString();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        LoadHighScore();
    }
}
