using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rig;
    public AudioClip SoundWalk;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Fall(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        rig.isKinematic = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(SoundWalk);
            animator.SetTrigger("Rotate");
            //rig.isKinematic = false;
            StartCoroutine(Fall(0.5f));
            Destroy(gameObject, 3.0f);
        }
    }
}