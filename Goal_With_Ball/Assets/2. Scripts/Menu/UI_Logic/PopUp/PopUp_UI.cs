using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp_UI : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;
    
    [SerializeField] private float activeTime = 1f;
    [SerializeField] private float disableTime = 1f;
    [SerializeField] private float startScale = 0;
    [SerializeField] private float endScale = 1f;

    private RectTransform rectTr;

    private void OnEnable()
    {
        Debug.Log("Setting awake");
        
        rectTr = GetComponent<RectTransform>();

        StartCoroutine(ActiveAnim());
    }

    public void PopDown_Btn()
    {
        StartCoroutine(DisableAnim());
    }

    private IEnumerator ActiveAnim()
    {
        foreach (var items in buttons)
        {
            items.interactable = false;
        }
        
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
        
        foreach (var items in buttons)
        {
            items.interactable = true;
        }
        
        this.gameObject.SetActive(false);
    }
}
