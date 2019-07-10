using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    public float speed = 0.1f;
    private bool play = false;
    private Rigidbody2D rig;
    public float jumpForce = 100;
    private float gravityNormal = 3.0f;
    private float gravityJump = 1.2f;
    private bool isGround = true;
    public Transform CheckPoint;
    private float groundDistance = 0.1f;
    public LayerMask GroundMask;

    public BoxCollider2D BoxCollider;
    public CircleCollider2D CircleCollider;

    public GameObject SmokePoint;
    public GameObject SmokeEffect;

    private bool isJumpDownHold = false;

    public GameObject StarHitEffect;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        StartCoroutine(CreateSmoke());
    }

    public void Jump()
    {
        if (isGround)
        {
            rig.gravityScale = gravityJump;
            rig.AddForce(new Vector2(0, jumpForce));
        }
    }

    IEnumerator CreateSmoke()
    {
        while (true)
        {
            if (isGround && isJumpDownHold && play)
            {
                Instantiate(SmokeEffect, SmokePoint.transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void JumpOff()
    {
        rig.gravityScale = gravityNormal;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Star"))
        {
            GameManager.Instance.Stars++;
            GameManager.Instance.Score += 10;
            Instantiate(StarHitEffect, gameObject.transform);
            Destroy(collision.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (play)
        {
            transform.Translate(new Vector2(speed, 0));

            var result = Physics2D.Raycast(CheckPoint.position, Vector2.down, groundDistance,GroundMask);
            if (result)
            {
                isGround = true;
                //Debug.Log(result.transform.name);
                animator.SetBool("IsGround",true);
            }
            else {
                isGround = false;
                animator.SetBool("IsGround", false);
            }
        }
        Debug.DrawLine(CheckPoint.position, CheckPoint.position + Vector3.down * groundDistance, Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            JumpOff();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Slide(true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Slide(false);
        }
    }

    public void Slide(bool slide)
    {
        animator.SetBool("Slide", slide);
        isJumpDownHold = slide;
        if (slide)
        {
            //Instantiate(SmokeEffect, SmokePoint.transform.position, Quaternion.identity);
            BoxCollider.enabled = false;
            CircleCollider.enabled = true;
        }
        else {
            BoxCollider.enabled = true;
            CircleCollider.enabled = false;
        }
    }

    public void Play()
    {
        play = true;
        animator.SetTrigger("Walk");
    }
}
