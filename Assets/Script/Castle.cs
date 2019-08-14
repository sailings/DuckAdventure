using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public Animator animator;
    public GameObject Lights;
    public Transform FirePos1;
    public Transform FirePos2;
    public Transform FirePos3;

    public GameObject[] FireWorks;

    // Start is called before the first frame update
    void Start()
    {
        Lights.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayEffect(float waitTime, Transform pos)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(FireWorks[Random.Range(0, FireWorks.Length)], pos.position, Quaternion.identity);
    }
    
    void PlayFirework()
    {
        StartCoroutine(PlayEffect(0.5f,FirePos1));
        StartCoroutine(PlayEffect(1.0f, FirePos2));
        StartCoroutine(PlayEffect(1.5f, FirePos3));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            PlayFirework();
            animator.SetTrigger("Close");
            Lights.SetActive(true);
            GameManager.Instance.GameSuccess();
        }
    }
}
