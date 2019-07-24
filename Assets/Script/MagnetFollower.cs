using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetFollower : MonoBehaviour
{
    private PlayerController controller;
    public float MoveSpeed = 5.0f;

    private void Awake()
    {
        controller = FindObjectOfType<PlayerController>();
        enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, controller.gameObject.transform.position, MoveSpeed * Time.deltaTime);
    }
}
