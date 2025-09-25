using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateTimeExtensions : MonoBehaviour
{
    public static bool IsFirstLoginThisWeek(DateTime? lastLoginTime, DayOfWeek weekStartsOn = DayOfWeek.Monday)
    {
        // 如果从未登录过，视为首次登录
        if (!lastLoginTime.HasValue) 
            return true;

        DateTime today = DateTime.Today;
        
        // 计算本周开始日期
        int daysSinceStartOfWeek = (7 + (today.DayOfWeek - weekStartsOn)) % 7;
        DateTime startOfWeek = today.AddDays(-daysSinceStartOfWeek);
        
        // 上次登录时间在本周开始之前
        return lastLoginTime.Value < startOfWeek;
    }
}
