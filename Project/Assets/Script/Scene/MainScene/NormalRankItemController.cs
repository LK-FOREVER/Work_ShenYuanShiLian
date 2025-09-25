using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class NormalRankItemController : MonoBehaviour
{
    NormalRanklistInfo rankInfo;
    public List<Sprite> rankImgs;//排名图片
    public List<Sprite> iconImgs;//头像
    public Image numberImg;//前三名，排名序号
    public Text numberTxt;//其他排名序号
    public Image headIcon;//头像
    public Text nameTxt;//姓名
    public Text levelTxt;//等级
    public Text normalLevelTxt;//通关进度
    private int currentChapter = 0;//当前章节
    private int currentLevel = 0;//当前关卡


    public void Init(NormalRanklistInfo rankInfo)
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

        if (rankInfo.normal_level == 81)
        {
            currentChapter = 4;
            currentLevel = 20;
        }
        else
        {
            currentChapter = (rankInfo.normal_level - 1) / 20 + 1;
            currentLevel = rankInfo.normal_level % 20 == 0 ? 20 : rankInfo.normal_level % 20;
        }

        normalLevelTxt.text = "通关进度：" + currentChapter + "-" + currentLevel;
    }
}