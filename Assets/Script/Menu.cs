using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text StarText;
    public Text LevelName;
    public Text BulletText;
    public GameObject Hearts;

    private int heartChildCount;

    public GameObject GamePause;

    // Start is called before the first frame update
    void Start()
    {
        heartChildCount = Hearts.transform.childCount;
        GamePause.SetActive(false);
    }

    void ShowHearts()
    {
        int hearts = GameManager.Instance.Hearts;
        for (int i = 0; i < heartChildCount; i++)
        {
            Hearts.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < hearts; i++)
        {
            Hearts.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void Pause()
    {
        GameManager.Instance.GameState = GameState.Pause;
        GamePause.SetActive(true);
        Time.timeScale = 0;
    }

    public void MainMenu()
    {
        GamePause.SetActive(false);
        GlobalValue.HitSavePoint = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        Time.timeScale = 1;
        GlobalValue.HitSavePoint = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        GameManager.Instance.GameState = GameState.Playing;
        GamePause.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        StarText.text = GameManager.Instance.Stars.ToString();
        BulletText.text = GameManager.Instance.Bullets.ToString();
        ShowHearts();
    }
}
