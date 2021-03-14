using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    //[SerializeField] Text pullDistance;
    [Header("Scene object reference")]
    [SerializeField] Canvas canvas;
    [SerializeField] Transform canvasTrans;
    [SerializeField] Text totalScoreText;
    [SerializeField] Camera UICamera;

    [Header("Prefab")]
    [SerializeField] GameObject pf_scoreText;

    UIScoreTextObjectPool pool_ScoreText;

    void Awake()
    {
        Instance = this;
        pool_ScoreText = new UIScoreTextObjectPool(pf_scoreText, canvas);
    }

    public void SpawnScoreText (Vector3 worldPosition, int score)
    {
        pool_ScoreText.Spawn(score.ToString(), worldPosition);
    }

    public void SetTotalScore (int amount)
    {
        totalScoreText.text = amount.ToString();
    }
}