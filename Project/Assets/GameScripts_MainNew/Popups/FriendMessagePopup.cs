using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendMessagePopup : MonoBehaviour
{
    public Image headIcon;
    public TextMeshProUGUI nickName;
    public TextMeshProUGUI level;
    public TextMeshProUGUI score;
    public TextMeshProUGUI challenge;
    public Sprite[] headIconSprites;
    
    public void Show(FriendClass currentFriend)
    {
        headIcon.sprite = headIconSprites[currentFriend.headIconIndex];
        nickName.text = currentFriend.name;
        level.text = "等级：" + currentFriend.level;
        score.text = "通关："+currentFriend.levelText;
        challenge.text = "领袖挑战最高伤害："+currentFriend.challenge.ToString();
    }
}
