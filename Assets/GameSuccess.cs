using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameSuccess : MonoBehaviour
{
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public Text ScoreText;
    public Text BestScoreText;

    public RectTransform Panel;
    private int tempScore;

    private void Awake()
    {
        Star1.SetActive(false);
        Star2.SetActive(false);
        Star3.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Panel.DOAnchorPosY(0, 1.5f).OnComplete(()=> {
            if (GameManager.Instance.Star > 0)
            {
                Star1.SetActive(true);
                Star1.transform.DOScale(3, 1).From().OnComplete(()=> {
                    if (GameManager.Instance.Star > 1)
                    {
                        Star2.SetActive(true);
                        Star2.transform.DOScale(3, 1).From().OnComplete(()=> {
                            if (GameManager.Instance.Star > 2)
                            {
                                Star3.SetActive(true);
                                Star3.transform.DOScale(3,1).From();
                            }
                        });
                    }
                });
            }
        });

        //ScoreText.text = GameManager.Instance.Score.ToString();

        var dot = DOTween.To(() => tempScore, x => tempScore = x, GameManager.Instance.Score, 3.0f);
        dot.OnUpdate(() => { ScoreText.text = tempScore.ToString(); });

        BestScoreText.text = GlobalValue.BestScore.ToString();

        //Star1.SetActive(GameManager.Instance.Star > 0);
        //Star2.SetActive(GameManager.Instance.Star > 1);
        //Star3.SetActive(GameManager.Instance.Star > 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
