using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private float activeTime = 1f;
    [SerializeField] private float disableTime = 1f;
    [SerializeField] private float startScale = 0;
    [SerializeField] private float endScale = 1f;

    private RectTransform rectTr;

    private void OnEnable()
    {
        rectTr = GetComponent<RectTransform>();
    }

    private void Start()
    {
        StartCoroutine(ActiveAnim());
    }

    public void QuitBtn()
    {
        StartCoroutine(DisableAnim());
    }

    public void NeverQuitBtn()
    {
        StartCoroutine(DisableAnim());
        SaveSystem.instance.saveData.isTutorialcheck = true;
        SaveSystem.instance.SaveGameData();
    }

    private IEnumerator ActiveAnim()
    {   
        var time = 0f;

        while (time < activeTime)
        {
            var scale = Mathf.Lerp(startScale, endScale, time / activeTime);
            rectTr.localScale = new Vector3(scale, scale, 1f);

            time += Time.deltaTime;
            yield return null;
        }
        
        rectTr.localScale = new Vector3(endScale, endScale, 1f);
    }
    
    private IEnumerator DisableAnim()
    {
        var time = 0f;

        while (time < disableTime)
        {
            var scale = Mathf.Lerp(endScale, startScale, time / disableTime);
            rectTr.localScale = new Vector3(scale, scale, 1f);

            time += Time.deltaTime;
            yield return null;
        }
        
        rectTr.localScale = new Vector3(startScale, startScale, 1f);
        
        this.gameObject.SetActive(false);
    }
}
