using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadingController : MonoBehaviour
{
    private AsyncOperation asyncLoad;
    public Image progressImage;
    private Scene changeToScene;
    public TextMeshProUGUI tips;
    private string[] tipsText = new[]
    {
        "每天都有一次免费开箱的机会，不要错过。",
        "遇到打不过的关卡，试着多强化装备。",
        "记得去商场转转，每天都有免费的金币礼包。",
        "挂机奖励上限12个小时，要及时领取。",
        "关卡中可以开启自动战斗，彻底解放双手。",
    };

    private void Awake()
    {
        progressImage.fillAmount = 0;
        changeToScene = Mvc.GetModel<GameModel>().NextScene;
        tips.text = tipsText[Random.Range(0,tipsText.Length)];
        StartCoroutine(LoadSceneAsyncCoroutine());
    }
    IEnumerator LoadSceneAsyncCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        asyncLoad = SceneManager.LoadSceneAsync((int)changeToScene);
        asyncLoad.allowSceneActivation = false;
         // 暂时不激活场景，直到加载完成
        float progress = 0;
        while (progress<1)
        {
            // 可以在这里添加加载进度条的逻辑
            progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // 因为加载大约在0.9处完成，所以这里做了一个小调整
            progressImage.fillAmount = progress;
            yield return null; // 等待下一帧
        }
        asyncLoad.allowSceneActivation = true;
        asyncLoad.completed += LoadComplete;
    }

    private void LoadComplete(AsyncOperation obj)
    {
        this.TriggerEvent(EventName.ChangeScene, new ChangeSceneEventArgs() { index = changeToScene });
        switch (changeToScene)
        {
            case Scene.Loading:
                Mvc.RegisterView(GameObject.Find("Canvas").GetComponent<LoadingSceneManager>());
                break;
            case Scene.Main:
                Mvc.RegisterView(GameObject.Find("Canvas").GetComponent<MainSceneManager>());
                //界面相关
                GameObject panels =  GameObject.Find("Panels");
                Mvc.RegisterView(panels.transform.Find("AdventurePanel").Find("Map").GetComponent<MapView>());
                Mvc.RegisterView(panels.transform.Find("SkillPanel").Find("SkillTree").GetComponent<SkillTreeView>());
                //弹窗相关
                GameObject popups = GameObject.Find("Popups");
                Mvc.RegisterView(popups.transform.Find("SettingPopup").GetComponent<SetBoard>());
                Mvc.RegisterView(popups.transform.Find("ChallengePopup").GetComponent<ChallengeBoard>());
                //提示相关
                GameObject tips = GameObject.Find("Tips");
                break;
            case Scene.Battle:
                Mvc.RegisterView(GameObject.Find("Canvas").GetComponent<BattleSceneManager>());
                Mvc.RegisterView(GameObject.Find("Canvas").transform.Find("CardOptionBoard").GetComponent<CardOptionBoard>());
                Mvc.RegisterView(GameObject.Find("PopupCanvas").transform.Find("DeadBoard").GetComponent<DeadBoard>());
                Mvc.RegisterView(GameObject.Find("PopupCanvas").transform.Find("SuccessBoard").GetComponent<SuccessBoard>());
                Mvc.RegisterView(GameObject.Find("PopupCanvas").transform.Find("SetBoard").GetComponent<SetBoard>());
                Mvc.RegisterView(GameObject.Find("PopupCanvas").transform.Find("challengeEndBoard").GetComponent<ChallengeEndBoard>());
                break;
            default:
                break;
        }
    }
}
