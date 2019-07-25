using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private PlayerController controller;
    public GameObject HitEffect;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<PlayerController>();
        if (GlobalValue.HitSavePoint)
        {
            controller.gameObject.transform.position = gameObject.transform.position;
            GameManager.Instance.Bullets = GlobalValue.SaveBullets;
            GameManager.Instance.Hearts = GlobalValue.SaveHearts;
            GameManager.Instance.Score = GlobalValue.SaveScore;
            GameManager.Instance.Stars = GlobalValue.SaveStars;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var effect = Instantiate(HitEffect,collision.gameObject.transform);
            effect.transform.localPosition = new Vector3(0.172f, 1.116f, 0);

            GlobalValue.HitSavePoint = true;
            GlobalValue.SaveBullets = GameManager.Instance.Bullets;
            GlobalValue.SaveHearts = GameManager.Instance.Hearts;
            GlobalValue.SaveScore = GameManager.Instance.Score;
            GlobalValue.SaveStars = GameManager.Instance.Stars;
            GlobalValue.IsUsingJetPack = controller.isUsingJetPack;

            string msg = string.Format("Bullets={0},Hearts={1},Score={2},Star={3}",GlobalValue.SaveBullets,GlobalValue.SaveHearts,GlobalValue.SaveScore,GlobalValue.SaveStars);
            Debug.Log(msg);

            //Debug.Break();
        }
    }
}