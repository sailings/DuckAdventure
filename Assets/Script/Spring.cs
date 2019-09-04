using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float PushForce = 700.0f;
    private Animator animator;
    private bool allowPush = true;

    public AudioClip SoundSpring;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && allowPush)
        {
            SoundManager.Instance.PlaySound(SoundSpring);
            allowPush = false;
            animator.SetTrigger("Push");
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,PushForce));
        }
    }
}
