using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private bool isRotate = false;
    private Animator animator;
    private PlayerController player;
    public GameObject Head;
    public float CannonForce = 1000.0f;
    public GameObject FirePoint;

    public AudioClip SoundCannon;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        Head.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotate && Input.anyKeyDown)
        {
            isRotate = false;
            animator.SetBool("Rotate", false);
            player.transform.position = FirePoint.transform.position;
            player.transform.rotation = FirePoint.transform.rotation;
            Head.SetActive(false);
            player.gameObject.SetActive(true);
            player.Play();
            SoundManager.Instance.PlaySound(SoundCannon);

            //Debug.Break();
            player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(CannonForce,0));
            player.transform.rotation = Quaternion.identity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isRotate)
        {
            isRotate = true;
            animator.SetBool("Rotate",true);
            player.gameObject.SetActive(false);
            Head.SetActive(true);
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
