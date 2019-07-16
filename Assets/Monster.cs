using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private float speed = -0.01f;
    public LayerMask TurnLayer;

    private bool dead = false;
    private bool playerDead = false;
    private PlayerController controller;

    private Vector3 effectPos1 = new Vector3(0.172f, 1.116f,0);
    private Vector3 effectPos2 = new Vector3(-0.938f, 0.846f,0);

    public GameObject KillByPlayerEffect;
    public GameObject KillPlayerEffect;

    public GameObject FireObject;

    private void Awake()
    {
        controller = FindObjectOfType<PlayerController>();
        if (FireObject)
        {
            FireObject.SetActive(false);
            StartCoroutine(FireIEnumer());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator FireIEnumer()
    {
        while (!dead)
        {
            yield return new WaitForSeconds(Random.Range(1.0f,3.0f));
            Fire();
            yield return new WaitForSeconds(Random.Range(1.0f, 1.5f));
            StopFire();
        }
    }

    private void Fire()
    {
        if (FireObject)
            FireObject.SetActive(true);
    }

    private void StopFire()
    {
        if (FireObject)
            FireObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector2(speed, 0));
        if (Physics2D.Raycast(gameObject.transform.position, Vector2.left, 0.1f, TurnLayer))
        {
            transform.Rotate(Vector3.up, 180);
        }
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + Vector3.left * 0.1f, Color.red);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerDead)
        {
            dead = true;
            //Debug.Log("Monster Dead");
            GameManager.Instance.Score += 100;

            var effect = Instantiate(KillByPlayerEffect, controller.gameObject.transform);
            effect.transform.localPosition = effectPos1;

            if (FireObject)
                FireObject.SetActive(false);

            gameObject.transform.localScale = new Vector3(1.0f,0.3f,1.0f);
            controller.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            controller.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,300));

            StartCoroutine(KillMonster(1.0f));
        }
    }

    IEnumerator KillMonster(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !dead)
        {
            playerDead = true;
            var effect = Instantiate(KillPlayerEffect, controller.gameObject.transform);
            effect.transform.localPosition = effectPos2;
            GameManager.Instance.GameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
