using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishDetect : MonoBehaviour
{
    public Fish fish;
    public GameObject AttackPoint;
    public float AttackDelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fish.transform.DOMove(AttackPoint.transform.position, AttackDelay);
            StartCoroutine(BeginAttack());
        }
    }

    IEnumerator BeginAttack()
    {
        yield return new WaitForSeconds(AttackDelay);
        fish.Attack();
    }
}
