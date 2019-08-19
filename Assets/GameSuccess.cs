using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSuccess : MonoBehaviour
{

    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public Text ScoreText;
    public Text BestScoreText;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("GameSuccess.Start");

        ScoreText.text = GameManager.Instance.Score.ToString();
        BestScoreText.text = GlobalValue.BestScore.ToString();

        Star1.SetActive(GameManager.Instance.Star > 0);
        Star2.SetActive(GameManager.Instance.Star > 1);
        Star3.SetActive(GameManager.Instance.Star > 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
