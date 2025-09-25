using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo
{
    public readonly int id;
    public readonly string name;
    public readonly string desc;
    public readonly int cost;
    public readonly int skillId;
}

public class UpgradeInfo
{
    public readonly int type;
    public readonly int grade;
    public readonly int hp;
    public readonly int mp;
    public readonly int atkMin;
    public readonly int atkMax;
    public readonly int atk;
    public readonly int price;
    public readonly float crit;
}

public class ExpUpgradeInfo
{
    public readonly int type;
    public readonly int grade;
    public readonly int hp;
    public readonly int atk;
    public readonly float dodge;
    public readonly float crit;
    public readonly int expCost;
}


public class EquipmentInfo
{
    public readonly int id;
    public readonly int type;
    public readonly string name;
    public readonly string desc;
    public readonly int dr;
    public readonly int priceMin;
    public readonly int priceMax;
}

public class EquipmentUpgradeInfo
{
    public readonly int id;
    public readonly int type;
    public readonly int grade;
    public readonly string name;
    public readonly string desc;
    public readonly float value;

    public readonly int price;
}

public class MonsterInfo
{
    public readonly int id;
    public readonly string name;
    public readonly int hp;
    public readonly int atk;
    public readonly float dex;
    public readonly float crit;
    public readonly int minIcon;
    public readonly int maxIcon;
    public readonly int exp;
}

public class LevelInfo
{
    public readonly int id;
    public readonly int monster;
    public readonly int chest;
    public readonly int empty;
}

public class MonsterWeight
{
    public readonly int id;
    public readonly int monster0;
    public readonly int monster1;
    public readonly int monster2;
    public readonly int monster3;
    public readonly int monster4;
    public readonly int monster5;
    public readonly int monster6;
    public readonly int monster7;
    public readonly int monster8;
    public readonly int monster9;
    public readonly int monster10;
    public readonly int monster11;
    public readonly int monster12;
    public readonly int monster13;
    public readonly int monster14;
    public readonly int monster15;
    public readonly int monster16;
    public readonly int monster17;
    public readonly int monster18;
    public readonly int monster19;
    public readonly int monster20;
    public readonly int monster21;
    public readonly int monster22;
    public readonly int monster23;
    public readonly int monster24;
    public readonly int monster25;
    public readonly int monster26;
    public readonly int monster27;
}

public class ChestWeight
{
    public readonly int id;
    public readonly int coin;
    public readonly int card;
}

public class CoinInfo
{
    public readonly int id;
    public readonly int coinMin;
    public readonly int coinMax;
}

public class CardWeight
{
    public readonly int id;
    public readonly int equipment;
    public readonly int commonBuff;
    public readonly int exclusiveBuff;
}

public class EquipmentWeight
{
    public readonly int id;
    public readonly int equipment0;
    public readonly int equipment1;
    public readonly int equipment2;
    public readonly int equipment3;
    public readonly int equipment4;
    public readonly int equipment5;
    public readonly int equipment6;
    public readonly int equipment7;
    public readonly int equipment8;
    public readonly int equipment9;
    public readonly int equipment10;
    public readonly int equipment11;
    public readonly int equipment12;
    public readonly int equipment13;
}

public class CommonBuffWeight
{
    public readonly int id;
    public readonly int buff0;
    public readonly int buff1;
    public readonly int buff2;
    public readonly int buff3;
    public readonly int buff4;
    public readonly int buff5;
    public readonly int buff6;
    public readonly int buff7;
    public readonly int buff8;
    public readonly int buff9;
    public readonly int buff10;
    public readonly int buff11;
    public readonly int buff12;
}

public class SoldierBuffWeight
{
    public readonly int id;
    public readonly int buff0;
    public readonly int buff1;
    public readonly int buff2;
    public readonly int buff3;
    public readonly int buff4;
    public readonly int buff5;
}

public class ArcherBuffWeight
{
    public readonly int id;
    public readonly int buff0;
    public readonly int buff1;
    public readonly int buff2;
    public readonly int buff3;
    public readonly int buff4;
    public readonly int buff5;
}

public class MasterBuffWeight
{
    public readonly int id;
    public readonly int buff0;
    public readonly int buff1;
    public readonly int buff2;
    public readonly int buff3;
}

public class SkillInfo
{
    public readonly int id;
    public readonly string name;
    public readonly string desc;
    public readonly List<int> configId;
    public readonly int time;
}

public class SkillConfig
{
    public readonly int id;
    public readonly int triggerTypeId;
    public readonly int selectorTypeId;
    public readonly int effectTypeId;
}

public class SkillTrigger
{
    public readonly int id;
    public readonly int triggerType;
}

public class SkillSelector
{
    public readonly int id;
    public readonly int selectorType;
    public readonly int targetTag;
}

public class SkillEffect
{
    public readonly int id;
    public readonly int effectType;
    public readonly List<int> param;
}

[System.Serializable]
public class MonsterData
{
    public int monsterId;
    public int count;
}

[System.Serializable]
public class LevelData
{
    public string levelId;
    public List<MonsterData> monsters;
}

public class ChapterData
{
    public int chapterId;
    public List<LevelData> levels;
}
public class SkillInfoData
{
    public int skillId;
    public string skillName;
    public string type;
    public int cooldown;
    public int duration;
    public int cost;
    public string description;
}

public enum EquipType
{
    Weapon,
    Necklace,
    Cloak,
    Head,
    Armor,
    Legs
}

public class EquipInfoData
{
    public int id;
    public string name;
    public float value;
    public string type;
    public string cnType;
    public string propertyType;
    public bool isPercent;
    public int quality;
    public int convertStones;
}

//额外提供的总属性
public class TotalProperty
{
    //攻击
    public int atk;
    //血量
    public int hp;
    //闪避
    public double dodgeRate;
    //暴击率
    public double critRate;
    //来自装备的增伤
    public double damageBonus;
    //来自装备的减伤
    public double damageReduction;
    //来自技能树的增伤
    public double skillDamageBonus;
    //来自技能树的减伤
    public double skillDamageReduction;
}

//装备本身提供的属性
public class EquipmentProperty
{
    //攻击
    public int attract;
    //血量
    public int hp;
    //闪避
    public float dodgeRate;
    //暴击率
    public float critRate;
    //来自装备的增伤
    public float damageBonus;
    //来自装备的减伤
    public int damageReduction;
}

//武器强化槽提供的额外属性
public class EquipmentLevelProperty
{
    //等级
    public int level;

    // 法杖攻击力（整数）
    public int weaponAttack;

    // 项链暴击率（百分比，存储为浮点数，如 5.0 表示 5.0%）
    public float necklaceCritRate;

    // 斗篷伤害增益（百分比）
    public float cloakDamageBonus;

    // 头盔生命值（整数）
    public int headHP;

    // 胸甲伤害减免（百分比）
    public float armorDamageReduction;

    // 腿甲闪避率（百分比）
    public float legsDodgeRate;

    // 强化消耗的金币
    public int goldCost;

    // 强化消耗的强化石
    public int stoneCost;
}

public class TaskInfoData
{
    public int id;
    public string desc;
    public int target;
    public string rewardType;
    public int rewardNum;
}
public class ShopItemInfo
{
    public int id;
    public string buyType;
    public int buyNum;
    public int buyLimit;
    public string buyDesc;
    public string limitType;
    public string costType;
    public int costNum;
}

public class SevenSignInfo
{
    public int day;
    public string rewardType;
    public int rewardNum;
}

public class NameData
{
    public List<Name> Name1;
    public List<Name> Name2;
}

public class AFKInfo
{
    public int chapter;
    public int coinPerMin;
    public int stonePer30Min;
    public int expPer30Min;
    public int diamondPer60Min;
}
public class Name
{
    public string name;
}

public class NormalRanklistInfo
{
    public int id;
    public int player_id;
    public int icon;
    public string name;
    public int level;//等级
    public int normal_level;//关卡
    public bool is_player;
}
public class ChallengeRanklistInfo
{
    public int id;
    public int player_id;
    public int icon;
    public string name;
    public int level;
    public int damage;
    public bool is_player;

    public override string ToString()
    {
        return $"ChallengeRanklistInfo: " +
               $"id={id}, " +
               $"player_id={player_id}, " +
               $"icon={icon}, " +
               $"name={name}, " +
               $"level={level}, " +
               $"damage={damage}, " +
               $"is_player={is_player}";
    }
}
public class ChallengeReward
{
    public string rewardType;
    public int rewardNum;
    public int quality;

}
public class ChallengeRewardInfo
{
    public int id;
    public string desc;
    public int target;
    public List<ChallengeReward> reward;
}