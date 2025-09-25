using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Treasure_Content : MonoBehaviour
{
    public Image equip;
    public Image quality;
    public TextMeshProUGUI num;

    public GameObject newObj;
    public GameObject convertObj;

    public TextMeshProUGUI convertText;
    public EquipInfoData thisEquip;
    
    
    public void InitContent(EquipInfoData curEquip)
    {
        thisEquip = curEquip;
        newObj.SetActive(false);
        convertObj.SetActive(false);
        equip.sprite = SpriteManager.Instance.GetEquipSprite(curEquip.type, curEquip.id);
        quality.sprite = SpriteManager.Instance.GetQualitySprite(curEquip.quality);
        if (num!=null)
        {
            num.gameObject.SetActive(false);
        }
    }

    public void ShowRandom(Sprite randomSprite, int thisQuality, int equipNum)
    {
        equip.sprite = randomSprite;
        quality.sprite = SpriteManager.Instance.GetQualitySprite(thisQuality);
        if (num!=null)
        {
            num.gameObject.SetActive(true);
            num.text = equipNum.ToString();
        }
    }

    public void PlayNew()
    {
        newObj.SetActive(true);
    }

    public void PlayConvert(int stoneNum)
    {
        GlobalTaskCounter.Instance.AddDailyCount(DailyTask.BreakEquip);
        GlobalTaskCounter.Instance.AddWeeklyCount(WeeklyTask.BreakEquip);
        convertObj.SetActive(true);  
        convertText.text = stoneNum.ToString();
    }

}
