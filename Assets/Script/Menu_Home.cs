using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Home : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject WorldChoose;

    public AudioClip SoundClick;

    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        WorldChoose.SetActive(false);
    }

    public void Play()
    {
        SoundManager.Instance.PlaySound(SoundClick);
        MainMenu.SetActive(false);
        WorldChoose.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SoundManager.Instance.PlaySound(SoundClick);
        WorldChoose.SetActive(false);
        MainMenu.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
