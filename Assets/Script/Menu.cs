using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text StarText;
    public Text LevelName;
    public Text BulletText;
    public GameObject Hearts;

    private int heartChildCount;

    //public GameObject Heart1;
    //public GameObject Heart2;
    //public GameObject Heart3;
    //public GameObject Heart4;
    //public GameObject Heart5;
    //public GameObject Heart6;
    //public GameObject Heart7;

    // Start is called before the first frame update
    void Start()
    {
        heartChildCount = Hearts.transform.childCount;
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
        //Heart1.SetActive(hearts >= 1);
        //Heart2.SetActive(hearts >= 2);
        //Heart3.SetActive(hearts >= 3);
        //Heart4.SetActive(hearts >= 4);
        //Heart5.SetActive(hearts >= 5);
        //Heart6.SetActive(hearts >= 6);
        //Heart7.SetActive(hearts >= 7);
    }
    
    // Update is called once per frame
    void Update()
    {
        StarText.text = GameManager.Instance.Stars.ToString();
        BulletText.text = GameManager.Instance.Bullets.ToString();
        ShowHearts();
    }
}
