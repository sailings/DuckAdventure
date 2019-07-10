using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text StarText;
    public Text ScoreText;

    public int Score { get; set; }
    public int Stars { get; set; }

    public static GameManager Instance;

    public PlayerController Controller;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.anyKeyDown)
        {
            Controller.Play();
        }

        StarText.text = "Star:"+Stars.ToString();
        ScoreText.text = "Score:"+Score.ToString();
    }
}
