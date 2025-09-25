using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GuidanceStateBase :MonoBehaviour
{
    public int stepID;
    [HideInInspector]
    public GameObject Content;
    [HideInInspector]
    public Button NextStateButton;

    private void Awake()
    {

        for (int i = 0; i < GuidanceStateManager.Instance.allStatesInScene.Count; i++)
        {
            if (GuidanceStateManager.Instance.allStatesInScene[i] == null)
            {
                GuidanceStateManager.Instance.allStatesInScene.Clear();
                break;
            }
        }
        if (!GuidanceStateManager.Instance.allStatesInScene.FirstOrDefault(state => state.stepID == stepID))
        {
            GuidanceStateManager.Instance.allStatesInScene.Add(this);
            Content = transform.Find("Content").gameObject;
            NextStateButton = transform.Find("NextStateButton").gameObject.GetComponent<Button>();
            Content.SetActive(false);
            NextStateButton.gameObject.SetActive(false);
        }
    }

    public virtual void OnEnter()
    {
        Content.SetActive(true);
        NextStateButton.gameObject.SetActive(true);
        NextStateButton.onClick.AddListener(OnMethod);
    }
    
    public virtual void OnMethod()
    {
        OnComplete();
    }
    
    public virtual void OnComplete()
    {
        Content.SetActive(false);
        NextStateButton.gameObject.SetActive(false);
        NextStateButton.onClick.RemoveListener(OnMethod);
        EventManager.Instance.TriggerEvent(EventName.GuidanceStepComplete,null,new ChangeStateArgs()
        {
            guidanceID = stepID,
        });
    }
}
