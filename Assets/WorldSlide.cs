using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WorldSlide : MonoBehaviour
{
    private float posx;

    public RectTransform Levels;
    private float step = 450;
    private float minX;
    private float maxX;
    private float blockCount = 2;
    private float speed = 0.8f;

    public GameObject WorldChoose;

    // Start is called before the first frame update
    void Start()
    {
        posx = Levels.anchoredPosition.x;
        maxX = posx;
        minX = maxX - (blockCount - 1) * step;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pre()
    {
        posx += step;
        posx = Mathf.Clamp(posx, minX, maxX);
        //Levels.anchoredPosition = new Vector2(posx, Levels.anchoredPosition.y);
        Levels.DOAnchorPosX(posx, speed);
    }

    public void Next()
    {
        posx -= step;
        posx = Mathf.Clamp(posx, minX, maxX);
        //Levels.anchoredPosition = new Vector2(posx, Levels.anchoredPosition.y);
        Levels.DOAnchorPosX(posx, speed);
    }

    public void Close()
    {
        WorldChoose.SetActive(true);
        gameObject.SetActive(false);
    }
}
