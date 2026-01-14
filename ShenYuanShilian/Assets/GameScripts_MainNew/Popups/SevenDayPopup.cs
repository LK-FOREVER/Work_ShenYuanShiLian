using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SevenDayPopup : MonoBehaviour
{
    public const string SignNumPrefs = "SignNum";
    public const string SignDataPrefs = "SignData";

    public GameObject[] alreadyHave;
    int signCount;
    DateTime today;
    DateTime lastSignDate;
    
    void Start()
    {
        
        // 初始化数据
        today = DateTime.Now;
        signCount = PlayerPrefs.GetInt(Mvc.GetModel<GameModel>().Account + SignNumPrefs, 0);
        lastSignDate = DateTime.Parse(PlayerPrefs.GetString(Mvc.GetModel<GameModel>().Account + SignDataPrefs, DateTime.MinValue.ToString()));
        
        // 检查是否需要重置
        if(NeedReset())
        {
            PlayerPrefs.DeleteKey(Mvc.GetModel<GameModel>().Account + SignNumPrefs);
            PlayerPrefs.DeleteKey(Mvc.GetModel<GameModel>().Account + SignDataPrefs);
            signCount = 0;
        }
        
        UpdateUI();
    }
    
    public void OnSignClicked()
    {
        if(!IsSameDay(lastSignDate, today))
        {
            signCount++;
            lastSignDate = today;
            
            PlayerPrefs.SetInt(Mvc.GetModel<GameModel>().Account + SignNumPrefs, signCount);
            PlayerPrefs.SetString(Mvc.GetModel<GameModel>().Account + SignDataPrefs, lastSignDate.ToString());
            
            GiveReward(signCount);
            UpdateUI();
        }
    }
    
    void UpdateUI()
    {
        for (int i = 0; i < alreadyHave.Length; i++)
        {
            alreadyHave[i].SetActive(i<PlayerPrefs.GetInt(Mvc.GetModel<GameModel>().Account + SignNumPrefs, 0));
        }
    }
    
    bool IsSameDay(DateTime date1, DateTime date2)
    {
        return date1.Year == date2.Year && 
               date1.Month == date2.Month && 
               date1.Day == date2.Day;
    }
    
    bool NeedReset()
    {
        // 超过7天或跨周重置
        if(signCount >= 7) return true;
        
        TimeSpan span = today - lastSignDate;
        return span.Days > 1 || GetWeekOfYear(today) != GetWeekOfYear(lastSignDate);
    }
    
    int GetWeekOfYear(DateTime date)
    {
        return System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
            date, 
            System.Globalization.CalendarWeekRule.FirstDay, 
            DayOfWeek.Monday);
    }
    
    void GiveReward(int day)
    {
        {
            var signInfo = DataManager.Instance.sevenSignList.FirstOrDefault(s => s.day == day);
            if (signInfo != null)
            {
                bool outEquip = Enum.TryParse(signInfo.rewardType, out RewardType awardType);
                EventManager.Instance.TriggerEvent(EventName.ShowCommonAward,null,new SetAward()
                {
                    awardList = new List<AwardInfo>()
                    {
                        new AwardInfo()
                        {
                            awardType = awardType,
                            awardNum = signInfo.rewardNum,
                        }
                    }
                });
            }
        }
    }
}
