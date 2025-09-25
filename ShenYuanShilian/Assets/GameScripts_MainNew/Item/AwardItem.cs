using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AwardInfo
{
    public RewardType awardType;
    public int awardNum;
    public EquipInfoData awardEquip;
}

public class AwardItem : MonoBehaviour
{
    public Image awardIcon;
    public TextMeshProUGUI awardNum;

    public void Init(Sprite icon, int num)
    {
        awardIcon.sprite = icon;
        awardNum.text = num.ToString();
    }
}
