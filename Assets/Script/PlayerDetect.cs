using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    public GameObject Monster;
    public bool InitEnable = false;

    private void Awake()
    {
        if(!InitEnable)
            Monster.SetActive(false);
    }

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
            Monster.SetActive(true);
            Monster.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}