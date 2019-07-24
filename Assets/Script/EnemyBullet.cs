using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 1.0f;
    private float minSpeed = 1.0f;
    private float maxSpeed = 2.0f;
    private bool isStop = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed,maxSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isStop = true;
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,300.0f));
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStop)
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
    }
}
