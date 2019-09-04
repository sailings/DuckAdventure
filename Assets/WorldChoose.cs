using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldChoose : MonoBehaviour
{
    public GameObject Lock2;
    public GameObject Lock3;
    public GameObject Block2;
    public GameObject Block3;

    public GameObject World1Levels;
    public GameObject World2Levels;
    public GameObject World3Levels;

    public GameObject World2;
    public GameObject World3;

    public AudioClip SoundClick;

    // Start is called before the first frame update
    void Start()
    {
        int worldReached = GlobalValue.WorldReached;
        if (worldReached > 1)
        {
            Lock2.SetActive(false);
            Block2.SetActive(false);
        }
        else {
            World2.GetComponent<EventTrigger>().enabled = false;
        }
        if (worldReached > 2)
        {
            Lock3.SetActive(false);
            Block3.SetActive(false);
        }
        else {
            World3.GetComponent<EventTrigger>().enabled = false;
        }
    }

    public void ShowWorld1Level()
    {
        SoundManager.Instance.PlaySound(SoundClick);
        GlobalValue.WorldPlaying = 1;
        gameObject.SetActive(false);
        World1Levels.SetActive(true);
    }

    public void ShowWorld2Level()
    {
        SoundManager.Instance.PlaySound(SoundClick);
        GlobalValue.WorldPlaying = 2;
        gameObject.SetActive(false);
        World2Levels.SetActive(true);
    }

    public void ShowWorld3Level()
    {
        SoundManager.Instance.PlaySound(SoundClick);
        GlobalValue.WorldPlaying = 3;
        gameObject.SetActive(false);
        World3Levels.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
