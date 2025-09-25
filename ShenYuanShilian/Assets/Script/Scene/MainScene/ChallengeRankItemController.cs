using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class ChallengeRankItemController : MonoBehaviour
{
    ChallengeRanklistInfo rankInfo;
    public List<Sprite> rankImgs;//排名图片
    public List<Sprite> iconImgs;//头像
    public Image numberImg;//前三名，排名序号
    public Text numberTxt;//其他排名序号
    public Image headIcon;//头像
    public Text nameTxt;//姓名
    public Text levelTxt;//等级
    public Text damageTxt;//伤害
    public void Init(ChallengeRanklistInfo rankInfo)
    {
        this.rankInfo = rankInfo;
        if (rankInfo.id <= 2)
        {
            numberImg.gameObject.SetActive(true);
            numberTxt.gameObject.SetActive(false);
            numberImg.sprite = rankImgs[rankInfo.id];
        }
        else
        {
            numberImg.gameObject.SetActive(false);
            numberTxt.gameObject.SetActive(true);
            numberTxt.text = (rankInfo.id + 1).ToString();
        }
        headIcon.sprite = iconImgs[rankInfo.icon];
        nameTxt.text = rankInfo.name;
        levelTxt.text = "等级：" + rankInfo.level;
        damageTxt.text = "伤害：" + rankInfo.damage;
    }
}