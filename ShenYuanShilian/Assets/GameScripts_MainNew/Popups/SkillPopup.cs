using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPopup : MonoBehaviour
{
   public TextMeshProUGUI skillName;
   public TextMeshProUGUI skillDuration;
   public TextMeshProUGUI skillCd;
   public TextMeshProUGUI skillDesc;
   public Image skillIcon;

   public void Show(ShowSkill e)
   {
      SkillInfoData thisSkill = null;
      foreach (var skill in DataManager.Instance.allskillShowInfoDataList)
      {
          if (skill.skillName == e.skillName)
          {
              thisSkill = skill;
              break;
          }
      }

      if (thisSkill != null)
      {
          skillName.text = thisSkill.skillName;
          skillDuration.text = thisSkill.duration == 0? "瞬发":thisSkill.duration+"秒";
          skillCd.text = thisSkill.cooldown.ToString()+"秒";
          skillDesc.text = thisSkill.description;
          skillIcon.sprite = Resources.Load<Sprite>("技能界面/" + thisSkill.skillName+"解锁");
      }
   }


}
