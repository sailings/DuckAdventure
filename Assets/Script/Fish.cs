using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private bool dead = false;
    private bool playerDead = false;
    private PlayerController controller;

    public AudioClip SoundHitPlayer;

    private void Awake()
    {
        controller = FindObjectOfType<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Debug.Log("ATTACK");
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0,300));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerDead)
        {
            dead = true;
            GameManager.Instance.Score += 100;
            //gameObject.transform.localScale = new Vector3(1.0f, 0.3f, 1.0f);
            controller.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            controller.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !dead)
        {
            SoundManager.Instance.PlaySound(SoundHitPlayer);
            playerDead = true;
            GameManager.Instance.GameOver();
        }
    }
}
