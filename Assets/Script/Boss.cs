using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float offsetPlayerX = 1.0f;
    private PlayerController controller;
    private bool isDead = false;
    public GameObject DropPoint;
    public GameObject[] DropMonsters;
    public float TotalLife = 100;
    private float lifeLeft;

    private void Awake()
    {
        lifeLeft = TotalLife;
        controller = FindObjectOfType<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        BeginAttack();
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Boss Hit");
            lifeLeft -= 10;
            if (lifeLeft <= 0)
            {
                Debug.Log("Boss Dead");
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }
}
