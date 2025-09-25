using System;
using System.Collections.Generic;
using UnityEngine;

public static class DeployerConfigFactory
{
    private static readonly Dictionary<string, object> cache; //对反射创建的实例进行缓存
    static DeployerConfigFactory()
    {
        cache = new Dictionary<string, object>();
    }

    public static PassiveSkillTrigger CreateTrigger(int triggerType, string type)
    {
        string classNameTrigger;
        switch (type)
        {
            case "Common":
                classNameTrigger = string.Format($"Common{Convert.ToString((CommonBuffTriggerType)triggerType)}Trigger");
                Type commonBuffType = Type.GetType(classNameTrigger);
                return Activator.CreateInstance(commonBuffType) as PassiveSkillTrigger;
            case "Soldier":
                classNameTrigger = string.Format($"Soldier{Convert.ToString((SoldierBuffTriggerType)triggerType)}Trigger");
                Type soldierBuffType = Type.GetType(classNameTrigger);
                return Activator.CreateInstance(soldierBuffType) as PassiveSkillTrigger;
            case "Archer":
                classNameTrigger = string.Format($"Archer{Convert.ToString((ArcherBuffTriggerType)triggerType)}Trigger");
                Type archerBuffType = Type.GetType(classNameTrigger);
                return Activator.CreateInstance(archerBuffType) as PassiveSkillTrigger;
            case "Master":
                classNameTrigger = string.Format($"Master{Convert.ToString((MasterBuffTriggerType)triggerType)}Trigger");
                Type masterBuffType = Type.GetType(classNameTrigger);
                return Activator.CreateInstance(masterBuffType) as PassiveSkillTrigger;
            default:
                return null;
        }
    }

    public static ISelector CreateSelector(int selectorType, string type)
    {
        string classNameSelector;
        switch (type)
        {
            case "Common":
                classNameSelector = string.Format($"Common{Convert.ToString((CommonBuffSelectorType)selectorType)}Selector");
                return CreateObject<ISelector>(classNameSelector);
            case "Soldier":
                classNameSelector = string.Format($"Soldier{Convert.ToString((SoldierBuffSelectorType)selectorType)}Selector");
                return CreateObject<ISelector>(classNameSelector);
            case "Archer":
                classNameSelector = string.Format($"Archer{Convert.ToString((ArcherBuffSelectorType)selectorType)}Selector");
                return CreateObject<ISelector>(classNameSelector);
            case "Master":
                classNameSelector = string.Format($"Master{Convert.ToString((MasterBuffSelectorType)selectorType)}Selector");
                return CreateObject<ISelector>(classNameSelector);
            default:
                return null;
        }
    }

    public static IEffect CreateEffect(int effectType, string type)
    {
        string classNameEffect;
        switch (type)
        {
            case "Common":
                classNameEffect = string.Format($"Common{Convert.ToString((CommonBuffEffectType)effectType)}Effect");
                return CreateObject<IEffect>(classNameEffect);
            case "Soldier":
                classNameEffect = string.Format($"Soldier{Convert.ToString((SoldierBuffEffectType)effectType)}Effect");
                return CreateObject<IEffect>(classNameEffect);
            case "Archer":
                classNameEffect = string.Format($"Archer{Convert.ToString((ArcherBuffEffectType)effectType)}Effect");
                return CreateObject<IEffect>(classNameEffect);
            case "Master":
                classNameEffect = string.Format($"Master{Convert.ToString((MasterBuffEffectType)effectType)}Effect");
                return CreateObject<IEffect>(classNameEffect);
            default:
                return null;
        }
    }

    private static T CreateObject<T>(string className) where T : class
    {
        if (!cache.ContainsKey(className))
        {
            Type type = Type.GetType(className);
            object instance = Activator.CreateInstance(type) as T;
            cache.Add(className, instance);
        }
        return cache[className] as T;
    }
}
