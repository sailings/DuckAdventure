using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public int levelNum = 1;
    public Text LevelText;

    // Start is called before the first frame update
    void Start()
    {
        int.TryParse(gameObject.name, out levelNum);
        LevelText.text = levelNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
