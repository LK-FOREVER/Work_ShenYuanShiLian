using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuidanceStateManager : MonoBehaviour
{
    private int guidanceId = 0;
    private int maxStep = 20;
    private int nextStep = 0;
    public List<GuidanceStateBase> allStatesInScene;
    public static GuidanceStateManager Instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += LoadComplete;
        EventManager.Instance.AddListener(EventName.GuidanceStepComplete,JudgeChangeScene);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= LoadComplete;
        EventManager.Instance.RemoveListener(EventName.GuidanceStepComplete,JudgeChangeScene);
    }

    private void JudgeChangeScene(object sender, EventArgs e)
    {
        ChangeStateArgs args = e as ChangeStateArgs;
        guidanceId = args.guidanceID;
        nextStep = args.guidanceID + 1;
        if (!args.changeScene)
        {
            ChangeState();
        }
    }

    private void ChangeState()
    {
        if (!Mvc.GetModel<GameModel>().FinishGuidance)
        {
            GuidanceStateBase nextState = allStatesInScene.FirstOrDefault(state => state.stepID == nextStep);
            nextState.OnEnter();
        }
    }
    
    private void LoadComplete(UnityEngine.SceneManagement.Scene arg0, LoadSceneMode arg1)
    {
        if (SceneManager.GetActiveScene()!= SceneManager.GetSceneByName("Change"))
        {
            if (!Mvc.GetModel<GameModel>().FinishGuidance && guidanceId>0)
            {
                nextStep = guidanceId + 1;
                StartCoroutine(WaitGuidanceListInit());
            }
        }
    }

    IEnumerator WaitGuidanceListInit()
    {
        yield return new WaitForEndOfFrame();
        ChangeState();
    }
}
