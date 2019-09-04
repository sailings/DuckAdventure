using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public Text StarText;
    //public Text ScoreText;
    //public Text HeartText;
    //public Text BulletText;

    //private GameState state;

    public int Score { get; set; }
    public int Stars { get; set; }

    public int Bullets { get; set; } = 2;

    private int hearts = 5;
    public int Hearts
    {
        get
        {
            return hearts;
        }
        set
        {
            hearts = Mathf.Clamp(value, 0, 7);
        }
    }

    public static GameManager Instance;

    public PlayerController Controller;

    private float heartReduceTime = 3.0f;

    public GameState GameState;

    private int totalScore;
    private int star1Score;
    private int star2Score;
    private int star3Score;

    public int Star;

    public AudioClip SoundGameOver;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(ReduceHeart());

        totalScore = Random.Range(500, 1000);
        star1Score = (int)(totalScore * 0.4f);
        star2Score = (int)(totalScore * 0.6f);
        star3Score = (int)(totalScore * 0.8f);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator ReduceHeart()
    {
        while (true)
        {
            yield return new WaitForSeconds(heartReduceTime);
            if(GameState == GameState.Playing)
                Hearts--;
            if (hearts <= 0)
            {
                //Debug.Log("Game Over");
                GameOver();
            }
        }
    }

    public void GameSuccess()
    {
        //Debug.Log("GameSuccess");

        Score = Random.Range(0, totalScore);

        if (GlobalValue.LevelPlaying >= GlobalValue.HightestLevel)
        {
            GlobalValue.HightestLevel = GlobalValue.LevelPlaying + 1;
        }

        if (GlobalValue.LevelPlaying ==  GlobalValue.MaxLevels[GlobalValue.WorldPlaying])
        {
            GlobalValue.WorldReached++;
        }

        if (Score >= star3Score)
        {
            GlobalValue.BestStar = 3;
            Star = 3;
        }
        else if (Score >= star2Score)
        {
            GlobalValue.BestStar = 2;
            Star = 2;
        }
        else if (Score >= star1Score)
        {
            GlobalValue.BestStar = 1;
            Star = 1;
        }
        else
        {
            GlobalValue.BestStar = 0;
            Star = 0;
        }

        if (Score > GlobalValue.BestScore)
            GlobalValue.BestScore = Score;

        GameState = GameState.Success;

        Debug.Log($"TotalScore={totalScore},Score={Score},Star={Star},BestScore={GlobalValue.BestScore}");

        StartCoroutine(Menu.Instance.ShowLevelComplete());
    }

    public void GameOver()
    {
        SoundManager.Instance.PlaySound(SoundGameOver);
        Controller.Dead();
        StartCoroutine(Restart(2.0f));
    }

    public void FallWaterGameOver(Vector3 floatPoint)
    {
        Controller.FallWater(floatPoint);
        StartCoroutine(Restart(1.5f));
    }

    IEnumerator Restart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Play()
    {
        GameState = GameState.Playing;
        Controller.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && GameState != GameState.Playing && GameState != GameState.Success)
        {
            Play();
        }

        if (Input.GetKeyDown(KeyCode.S) && GameState == GameState.Playing)
        {
            GameSuccess();
        }

        //StarText.text = "Star:"+Stars.ToString();
        //ScoreText.text = "Score:"+Score.ToString();
        //HeartText.text = "Heart:" + Hearts.ToString();
        //BulletText.text = "Bullet:" + Bullets.ToString();
    }
}

public enum GameState
{
    Menu,
    Playing,
    Pause,
    Success
}
