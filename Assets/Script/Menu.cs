﻿using System.Collections;
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
    public GameObject LevelComplete;

    public static Menu Instance;

    public AudioClip SoundSuccess;
    public AudioClip SoundClick;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        heartChildCount = Hearts.transform.childCount;
        GamePause.SetActive(false);
        LevelComplete.SetActive(false);
        LevelName.text = $"WORLD {GlobalValue.WorldPlaying}-{GlobalValue.LevelPlaying}";
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
        SoundManager.Instance.PlaySound(SoundClick);
        GameManager.Instance.GameState = GameState.Pause;
        GamePause.SetActive(true);
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        SoundManager.Instance.PlaySound(SoundClick);
        GlobalValue.HitSavePoint = false;

        if (GlobalValue.WorldPlaying <= 3)
        {
            if (GlobalValue.LevelPlaying < GlobalValue.MaxLevels[GlobalValue.WorldPlaying])
                GlobalValue.LevelPlaying++;
            else {
                GlobalValue.WorldPlaying++;
                GlobalValue.LevelPlaying = 1;
            }
            if(GlobalValue.WorldPlaying <=3 && GlobalValue.LevelPlaying<=GlobalValue.MaxLevels[GlobalValue.WorldPlaying])
                SceneManager.LoadScene($"W{GlobalValue.WorldPlaying}-{GlobalValue.LevelPlaying}");
        }
    }

    public void MainMenu()
    {
        SoundManager.Instance.PlaySound(SoundClick);
        GamePause.SetActive(false);
        GlobalValue.HitSavePoint = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        SoundManager.Instance.PlaySound(SoundClick);
        Time.timeScale = 1;
        GlobalValue.HitSavePoint = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        SoundManager.Instance.PlaySound(SoundClick);
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

    public IEnumerator ShowLevelComplete()
    {
        yield return new WaitForSeconds(2);
        SoundManager.Instance.PlaySound(SoundSuccess);
        LevelComplete.SetActive(true);
    }
}
