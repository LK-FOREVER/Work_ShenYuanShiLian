using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChallengeEndBoard : View
{
    public override string Name
    {
        get
        {
            return Consts.V_ChallengeEndBoard;
        }
    }
    [SerializeField]
    private Text challengeEndBoardDamage;
    [SerializeField]
    private GameObject challengeEndBoardNewRecord;
    [SerializeField]
    private Button challengeEndBoardAgainBtn;
    [SerializeField]
    private Button challengeEndBoardExitBtn;

    private void OnEnable()
    {
        challengeEndBoardAgainBtn.onClick.AddListener(OnClickAgainBtn);
        challengeEndBoardExitBtn.onClick.AddListener(OnClickExitBtn);
        InitUI();
    }
    private void InitUI()
    {
        int damage = GameObject.Find("Canvas").GetComponent<BattleSceneManager>().GetChallengeCurrentDamage();
        challengeEndBoardDamage.text = damage.ToString();
        challengeEndBoardNewRecord.SetActive(damage > GetModel<GameModel>().LeaderChallengeDamage);
    }

    private void OnClickAgainBtn()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        Time.timeScale = 1;
        Utils.FadeIn();
        GameManager.Instance.ChangeScene(Scene.Battle);
    }
    private void OnClickExitBtn()
    {
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        Time.timeScale = 1;
        Utils.FadeIn();
        GameManager.Instance.ChangeScene(Scene.Main);
    }
    public override void HandleEvent(object data = null)
    {
    }
}
