using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatAccountPopup : MonoBehaviour
{
    [HideInInspector]
    public string nickName;
    public TMP_InputField InputField;
    public TextMeshProUGUI tipsText;

    public void CheckName()
    {
        nickName = InputField.text;
        if (nickName.Length  == 0)
        {
            tipsText.gameObject.SetActive(true);
            tipsText.text = "昵称不能为空。";
            return;
        }
       
        if (nickName.Length > 6)
        {
            tipsText.gameObject.SetActive(true);
            tipsText.text = "昵称不能超过6个字符。";
            return;
        }

        if (ContainsForbiddenWord(nickName))
        {
            tipsText.gameObject.SetActive(true);
            tipsText.text = "昵称包含敏感词，请重新输入。";
            return;
        }

        Mvc.GetModel<GameModel>().NickName = nickName;
        this.gameObject.SetActive(false);
        EventManager.Instance.TriggerEvent(EventName.RefreshTopMessage);
        if (!Mvc.GetModel<GameModel>().FinishGuidance)
        {
            //开始新手引导
            EventManager.Instance.TriggerEvent(EventName.GuidanceStepComplete,null,new ChangeStateArgs()
            {
                guidanceID = 0
            });
        }
    }
    
    // 检查昵称是否包含屏蔽词
    public bool ContainsForbiddenWord(string nickname)
    {
        string lowerNickname = nickname.ToLower();
        foreach (string word in DataManager.Instance.forbiddenWords)
        {
            if (lowerNickname.Contains(word))
            {
                if (word == "")
                {
                    continue;
                }
                Debug.Log($"昵称包含屏蔽词: {word}");
                return true;
            }
        }
        return false;
    }
}
