using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private float speed = -0.01f;
    public LayerMask TurnLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector2(speed, 0));
        if (Physics2D.Raycast(gameObject.transform.position, Vector2.left, 0.1f, TurnLayer))
        {
            transform.Rotate(Vector3.up, 180);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
