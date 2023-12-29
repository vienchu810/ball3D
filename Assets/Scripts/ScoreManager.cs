using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private Text scoreText;
    public int score = 10;
    private void Awake()
    {
        MakeSingleton();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }
    private void Start()
    {
        AddScore(0);
    }
    private void Update()
    {
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            scoreText.text=score.ToString();
        }
    }
    void MakeSingleton()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
        if(score>PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }    
        scoreText.text=score.ToString() ;
    }    

    public void ResetScore()
    {
        score = 0;
    }

}
