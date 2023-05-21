using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OBJ_NameTool : ScriptableWizard
{
    public string baseName = "Obj_"; // 기본 이름
    public int startNum = 0; // 시작 숫자
    public int increse = 1; // 증가치

    [MenuItem("Edit/이름 바꾸기")]
    static void createWizard()
    {
        ScriptableWizard.DisplayWizard("이름 바꾸기", typeof(OBJ_NameTool), "바꾸기");
    }

    // 처음 나타날 때 호출
    private void OnEnable()
    {
        UpdateSelectionHelper();
    }

    // 씬에서 선택 영역이 변경될 때 호출
    private void OnSelectionChange()
    {
        UpdateSelectionHelper();
    }

    // 선택된 갯수를 업데이트
    void UpdateSelectionHelper()
    {
        helpString = "";

        if (Selection.objects != null)
            helpString = "선택된 오브젝트 갯수 : " + Selection.objects.Length;
    }

    // 이름 변경
    private void OnWizardCreate()
    {
        if (Selection.objects == null)
            return;
        
        //현재 증가치
        var postFix = startNum;

        // 순회하며 이름 변경
        foreach (var o in Selection.objects)
        {
            o.name = baseName + postFix;
            postFix += increse;
        }
    }
}
