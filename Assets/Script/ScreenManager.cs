using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private float scaleX, scaleY;
    private Camera cam;
    public GameObject Cloud;
    public GameObject Mountain;
    public GameObject Tree;

    private Material cloudMat;
    private Material mountainMat;
    private Material treeMat;

    public float CloudSpeed = 1.0f;
    public float MountainSpeed = 1.0f;
    public float TreeSpeed = 1.0f;

    private float offsetX;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cloudMat = Cloud.GetComponent<MeshRenderer>().material;
        mountainMat = Mountain.GetComponent<MeshRenderer>().material;
        treeMat = Tree.GetComponent<MeshRenderer>().material;

        offsetX = Camera.main.transform.position.x;
    }

    void AdapterScreen()
    {
        scaleY = cam.orthographicSize * 2;
        scaleX = (Screen.width*1.0f / Screen.height) * scaleY;
        Cloud.transform.localScale = new Vector3(scaleX, scaleY, 1);
        Mountain.transform.localScale = new Vector3(scaleX, 1, 1);
        Tree.transform.localScale = new Vector3(scaleX, 1, 1);
    }

    void MoveForward()
    {
        var offSet = Camera.main.transform.position.x - offsetX;
        cloudMat.mainTextureOffset = new Vector2(offSet * CloudSpeed, cloudMat.mainTextureOffset.y);
        mountainMat.mainTextureOffset = new Vector2(offSet * MountainSpeed, mountainMat.mainTextureOffset.y);
        treeMat.mainTextureOffset = new Vector2(offSet * TreeSpeed,treeMat.mainTextureOffset.y);
    }

    // Update is called once per frame
    void Update()
    {
        AdapterScreen();
        MoveForward();
    }
}
