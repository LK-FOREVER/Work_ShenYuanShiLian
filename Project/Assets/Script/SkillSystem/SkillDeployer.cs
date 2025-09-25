using System.Collections.Generic;
using UnityEngine;

public class SkillConfigData
{
    public PassiveSkillTrigger trigger;
    public ISelector selector;
    public IEffect effect;
}

public class SkillDeployer : MonoBehaviour
{
    public int skillId;

    public string type;

    private List<int> configIdList;

    private Dictionary<int, SkillConfigData> skillConfigDic = new();

    SkillConfigData config;

    private GameObject[] targets;

    public void InitDeployer(int _skillId, string _type)
    {
        skillId = _skillId;
        type = _type;
        if(skillId == -1) return;
        configIdList = DataManager.Instance.skillInfoDic[type][skillId].configId;

        for (int i = 0; i < configIdList.Count; i++)
        {
            SkillConfig configId = DataManager.Instance.skillConfigDic[type][configIdList[i]];
            config = new SkillConfigData();
            config.trigger = DeployerConfigFactory.CreateTrigger(DataManager.Instance.skillTriggerDic[type][configId.triggerTypeId].triggerType, type);
            config.selector = DeployerConfigFactory.CreateSelector(DataManager.Instance.skillSelectorDic[type][configId.selectorTypeId].selectorType, type);
            config.effect = DeployerConfigFactory.CreateEffect(DataManager.Instance.skillEffectDic[type][configId.effectTypeId].effectType, type);
            skillConfigDic.Add(configIdList[i], config);
            config.trigger.Init(this, configIdList[i]);
        }
    }

    protected void OnDestroy()
    {
        for (int i = 0; i < configIdList.Count; i++)
        {
            EndSkill(configIdList[i]);
        }

        foreach (int key in skillConfigDic.Keys)
        {
            skillConfigDic[key].trigger.RemoveListener();
            skillConfigDic[key].trigger = null;
            skillConfigDic[key].selector = null;
            skillConfigDic[key].effect = null;
        }

        skillConfigDic.Clear();
    }

    public void CalculateTargets(int id)
    {
        SkillConfig skillConfig = DataManager.Instance.skillConfigDic[type][id];
        int param = DataManager.Instance.skillSelectorDic[type][skillConfig.selectorTypeId].targetTag;
        targets = skillConfigDic[id].selector.SelectTarget(gameObject, param);
    }

    public void ImpactTargets(int id)
    {
        SkillConfig skillConfig = DataManager.Instance.skillConfigDic[type][id];
        List<int> param = DataManager.Instance.skillEffectDic[type][skillConfig.effectTypeId].param;
        skillConfigDic[id].effect.Execute(gameObject, targets, param);
    }

    public void RemoveEffect(int id)
    {
        SkillConfig skillConfig = DataManager.Instance.skillConfigDic[type][id];
        List<int> param = DataManager.Instance.skillEffectDic[type][skillConfig.effectTypeId].param;
        skillConfigDic[id].effect.EndSkill(gameObject, targets, param);
    }

    public void DeploySkill(int id)
    {
        CalculateTargets(id);
        ImpactTargets(id);
    }

    public void EndSkill(int id)
    {
        CalculateTargets(id);
        RemoveEffect(id);
    }
}
