using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float RotateSpeed = 100.0f;
    private float minSpeed = 50.0f;
    private float maxSpeed = 150.0f;
    // Start is called before the first frame update
    void Start()
    {
        RotateSpeed = Random.Range(minSpeed,maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), RotateSpeed * Time.deltaTime);
    }
}