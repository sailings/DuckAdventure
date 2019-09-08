using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    private float groundDistance = 0.2f;
    public LayerMask GroundMask;
    private bool die = false;

    public BoxCollider2D BoxCollider;
    public CircleCollider2D CircleCollider;

    public GameObject SmokePoint;
    public GameObject SmokeEffect;

    private bool isJumpDownHold = false;

    public GameObject StarHitEffect;
    public GameObject FruitHitEffect;
    public GameObject BloodEffect;

    public GameObject Magnet;
    public float MagnetTimer = 10.0f;

    public GameObject JetPack;
    public bool isUsingJetPack = false;
    private bool isJumpUpHold = false;

    public GameObject ThrownPoint;
    public GameObject Bullet;

    public AudioClip SoundJump;
    public AudioClip SoundCollect;
    public AudioClip SoundMagnet;
    public AudioClip SoundEatFruit;
    public AudioClip SoundFallWater;
    public AudioClip SoundAttack;
    public AudioClip SoundNoBullet;
    public AudioClip SoundHitEnemy;

    private void Awake()
    {
        Magnet.SetActive(false);
        JetPack.SetActive(false);
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        StartCoroutine(CreateSmoke());
    }

    public void Jump()
    {
        isJumpUpHold = true;
        if (isGround)
        {
            SoundManager.Instance.PlaySound(SoundJump);
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
        isJumpUpHold = false;
        rig.gravityScale = gravityNormal;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GlobalValue.HitSavePoint)
        {
            isUsingJetPack = GlobalValue.IsUsingJetPack;
            JetPack.SetActive(isUsingJetPack);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Saw"))
        {
            Instantiate(BloodEffect, transform);
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Star"))
        {
            SoundManager.Instance.PlaySound(SoundCollect);
            GameManager.Instance.Stars++;
            GameManager.Instance.Score += 10;
            Instantiate(StarHitEffect, gameObject.transform);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Fruit"))
        {
            SoundManager.Instance.PlaySound(SoundEatFruit);
            GameManager.Instance.Hearts++;
            Instantiate(FruitHitEffect, gameObject.transform);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SoundManager.Instance.PlaySound(SoundHitEnemy);
            GameManager.Instance.GameOver();
            //Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Magnet"))
        {
            SoundManager.Instance.PlaySound(SoundMagnet);
            Magnet.SetActive(true);
            Debug.Log("Hit Magnet");
            Destroy(collision.gameObject);
            StartCoroutine(DisableMagnet());
        }
        if (collision.gameObject.CompareTag("JetPack"))
        {
            SoundManager.Instance.PlaySound(SoundCollect);
            isUsingJetPack = true;
            JetPack.SetActive(true);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("BulletCollect"))
        {
            SoundManager.Instance.PlaySound(SoundCollect);
            GameManager.Instance.Bullets += 3;
            Destroy(collision.gameObject);
        }
    }

    IEnumerator DisableMagnet()
    {
        yield return new WaitForSeconds(MagnetTimer);
        Magnet.SetActive(false);
    }

    public void Dead()
    {
        if (!die)
        {
            die = true;
            animator.SetTrigger("Die");
            BoxCollider.enabled = false;
            CircleCollider.enabled = false;
            rig.velocity = Vector2.zero;
            rig.gravityScale = 0.5f;
        }
    }

    public void FallWater(Vector3 floatPoint)
    {
        if (!die)
        {
            SoundManager.Instance.PlaySound(SoundFallWater);
            die = true;
            rig.velocity = Vector3.zero;
            rig.isKinematic = true;
            transform.DORotate(new Vector3(0, 0, 90), 0.5f).OnComplete(()=> {
                transform.DOMoveY(floatPoint.y, 0.5f);
            });
        }
    }

    private void FixedUpdate()
    {
        if (play && !die)
        {
            transform.Translate(new Vector2(speed, 0));

            if (isUsingJetPack && isJumpUpHold)
            {
                rig.velocity = Vector2.zero;
                rig.AddForce(new Vector2(0,150.0f));
            }

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
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (!die)
        {
            if (GameManager.Instance.Bullets > 0)
            {
                SoundManager.Instance.PlaySound(SoundAttack);
                animator.SetTrigger("Thrown");
                var newBullet = Instantiate(Bullet, ThrownPoint.transform.position, Quaternion.identity);
                newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(800, 100));
                GameManager.Instance.Bullets--;
            }
            else {
                SoundManager.Instance.PlaySound(SoundNoBullet);
            }
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
