using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text StarText;
    public Text ScoreText;
    public Text HeartText;
    public Text BulletText;

    private GameState state;

    public int Score { get; set; }
    public int Stars { get; set; }

    public int Bullets { get; set; } = 200;

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

    private void Awake()
    {
        Instance = this;
        StartCoroutine(ReduceHeart());
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
            if(state == GameState.Playing)
                Hearts--;
            if (hearts <= 0)
            {
                //Debug.Log("Game Over");
                //GameOver();
            }
        }
    }

    public void GameOver()
    {
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
        state = GameState.Playing;
        Controller.Play();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.anyKeyDown)
        {
            Play();
        }

        StarText.text = "Star:"+Stars.ToString();
        ScoreText.text = "Score:"+Score.ToString();
        HeartText.text = "Heart:" + Hearts.ToString();
        BulletText.text = "Bullet:" + Bullets.ToString();
    }
}

public enum GameState
{
    Menu,
    Playing,
    Pause
}
