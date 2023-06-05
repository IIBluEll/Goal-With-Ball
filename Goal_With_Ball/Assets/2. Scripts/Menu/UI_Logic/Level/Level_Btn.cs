using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Level_Btn : MonoBehaviour
{
   [SerializeField] private Level_Datas levelData;
   [SerializeField] private GameObject clearIcon;

   [SerializeField] private Sprite[] btn_Sprites; // 0 - 스테이지 깸 / 1 - 현재 스테이지 / 2 - 비활성화
   
   private TextMeshProUGUI stageTxt;
   private Image btnImg;
   private Button thisbtn;
   private void Start()
   {
      thisbtn = GetComponent<Button>();
      btnImg = GetComponent<Image>();
      stageTxt = GetComponentInChildren<TextMeshProUGUI>();
      stageTxt.text = $"{levelData.levelNumber}";

      var nowstage = SaveSystem.instance.saveData.nowStage;
      var nextstage = SaveSystem.instance.saveData.nextStage;

      if (levelData.levelNumber <= nowstage)
      {
         clearIcon.SetActive(true);
         btnImg.sprite = btn_Sprites[0];
         thisbtn.interactable = true;
      }
      else if (levelData.levelNumber == nextstage)
      {
         btnImg.sprite = btn_Sprites[1];
         thisbtn.interactable = true;
      }
      else
      {
         btnImg.sprite = btn_Sprites[2];
      }
   }

   public void OnClick()
   {
      var newObj = new GameObject().AddComponent<LoadLevelManager>();
      newObj.levelDatas = levelData;
      newObj.name = "LevelLoad";
    
      TempSceneChange();
   }
   private void TempSceneChange()
   {
      //ToDo : 비동기 씬 전환 만들기
      SceneManager.LoadScene("InGame");
   }
   
   
}d
