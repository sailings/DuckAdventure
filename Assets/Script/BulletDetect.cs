using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDetect : MonoBehaviour
{
    public GameObject BigBullets;

    public AudioClip SoundBullets;
    // Start is called before the first frame update
    void Start()
    {
        BigBullets.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(SoundBullets);
            Debug.Log("Bullet Detected Player");
            BigBullets.SetActive(true);
        }
    }
}