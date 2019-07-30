using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float offsetPlayerX = 1.0f;
    private PlayerController controller;
    private bool isDead = false;
    public GameObject DropPoint;
    public GameObject[] DropMonsters;
    public float TotalLife = 100;
    private float lifeLeft;
    private bool isProtected = false;
    public GameObject[] ProtectEffect;

    public GameObject DeadEffect;
    public GameObject HitEffect;
    public GameObject BulletMissEffect;

    public GameObject SmokeEffect;

    public Slider slider;

    private void Awake()
    {
        SmokeEffect.SetActive(false);
        lifeLeft = TotalLife;
        controller = FindObjectOfType<PlayerController>();
        for (int i = 0; i < ProtectEffect.Length; i++)
        {
            ProtectEffect[i].SetActive(false);
        }
    }

    IEnumerator ProtectBoss()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(Random.Range(1.0f,2.0f));
            isProtected = true;
            controller = FindObjectOfType<PlayerController>();
            for (int i = 0; i < ProtectEffect.Length; i++)
            {
                ProtectEffect[i].SetActive(false);
            }
            ProtectEffect[Random.Range(0, ProtectEffect.Length)].SetActive(true);
            yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));
            isProtected = false;
            controller = FindObjectOfType<PlayerController>();
            for (int i = 0; i < ProtectEffect.Length; i++)
            {
                ProtectEffect[i].SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        BeginAttack();
        //StartCoroutine(ProtectBoss());
    }

    IEnumerator Attack()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(Random.Range(2.0f,3.0f));
            //Instantiate(DropMonsters[Random.Range(0, DropMonsters.Length)],DropPoint.transform.position,Quaternion.identity);
        }
    }
    public void BeginAttack()
    {
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(controller.gameObject.transform.position.x + offsetPlayerX, transform.position.y);
        slider.value = (lifeLeft / TotalLife) * slider.maxValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (!isProtected)
            {
                Debug.Log("Boss Hit");
                lifeLeft -= 10;
                if (lifeLeft > 0 && lifeLeft / TotalLife <= 0.7f)
                {
                    SmokeEffect.SetActive(true);
                    var newColor = lifeLeft / TotalLife;
                    SmokeEffect.GetComponent<ParticleSystem>().startColor = new Color(newColor,newColor,newColor);
                }
                var hitEffect = Instantiate(HitEffect, collision.gameObject.transform.position, Quaternion.identity);
                hitEffect.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                if (lifeLeft <= 0)
                {
                    Debug.Log("Boss Dead");
                    isDead = true;
                    GameManager.Instance.Score += 500;
                    Instantiate(DeadEffect, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
            else {
                Debug.Log("Boss Protect");
                var missEffect = Instantiate(BulletMissEffect,collision.gameObject.transform.position,Quaternion.identity);
                missEffect.transform.position = collision.gameObject.transform.position - new Vector3(1, 0, 0);
            }
            Destroy(collision.gameObject);
        }
    }
}
