using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public int levelNum = 1;
    public Text LevelText;

    public GameObject Lock;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    // Start is called before the first frame update
    void Start()
    {
        int.TryParse(gameObject.name, out levelNum);
        LevelText.text = levelNum.ToString();

        if (GlobalValue.HightestLevel < levelNum)
        {
            Lock.SetActive(true);
            GetComponent<EventTrigger>().enabled = false;
        }
        else {
            Lock.SetActive(false);
        }
        int stars = PlayerPrefs.GetInt("World" +GlobalValue.WorldPlaying + "-" + levelNum + "BestStar", 0);
        star1.SetActive(stars > 0);
        star2.SetActive(stars > 1);
        star3.SetActive(stars > 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        GlobalValue.LevelPlaying = levelNum;
        SceneManager.LoadScene($"W{GlobalValue.WorldPlaying}-{GlobalValue.LevelPlaying}");
    }
}
