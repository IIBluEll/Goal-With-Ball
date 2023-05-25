using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// 1. 공의 life Count가 변함을 감지하고 TXT 변경
/// </summary>
public class BallLifeCountTXT : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lifeCount;
    [SerializeField] private Ball_Ctl playerBall;
    [SerializeField] private InGameManager inGameManager;

    private RectTransform rectTR;
    
    [SerializeField] private bool isAnim = false;
    private void Start()
    {
        #region Debug Assert

        Debug.Assert(inGameManager,$"[{this.gameObject.name}] don't have InGameManager : Null Ref");
        Debug.Assert(lifeCount,$"[{this.gameObject.name}] don't have lifeCountText : Null Ref");

        #endregion

        rectTR = GetComponent<RectTransform>();
        
        if (inGameManager != null)
        {
            lifeCount.text = $"{inGameManager.Life}";

            inGameManager.gameOver += BallDie;
            inGameManager.ChangeLife += ChangeLifeValue;
        }
    }

    private void ChangeLifeValue(int life) => StartCoroutine(TextAnimation(life));

    private void BallDie() => lifeCount.text = "Player Die!";

    private IEnumerator TextAnimation(int life)
    {
        var duration = 0.05f;

        var originPos = rectTR.anchoredPosition;
        var x = originPos.x;
        var y = originPos.y;

        
        lifeCount.text = $"{life}";
        
        if (!isAnim)
        {
            Debug.Log("Txt Anim 시작");
            isAnim = true;

            //TODO : 나중에 효율적으로 바꿔보자
            rectTR.anchoredPosition = new Vector2(x + Random.Range(-50f, 50f), y);
            yield return new WaitForSeconds(duration);
            rectTR.anchoredPosition = originPos;
            yield return new WaitForSeconds(duration);
            rectTR.anchoredPosition = new Vector2(x, y + Random.Range(-50f, 50f));
            yield return new WaitForSeconds(duration);
            rectTR.anchoredPosition = originPos;
            yield return new WaitForSeconds(duration);
            rectTR.anchoredPosition = new Vector2(x + Random.Range(-50f, 50f), y);
            yield return new WaitForSeconds(duration);
            rectTR.anchoredPosition = originPos;
            yield return new WaitForSeconds(duration);
            rectTR.anchoredPosition = new Vector2(x, y + Random.Range(-50f, 50f));
            yield return new WaitForSeconds(duration);
            rectTR.anchoredPosition = originPos;
            yield return new WaitForSeconds(duration);
            
            isAnim = false;
        }
    }

    #region Event 구독 해제

    private void OnDisable()
    {
        inGameManager.gameOver -= BallDie;
        inGameManager.ChangeLife -= ChangeLifeValue;
    }

    #endregion
   
}
