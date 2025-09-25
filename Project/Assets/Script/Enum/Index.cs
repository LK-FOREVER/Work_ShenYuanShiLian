public enum Scene
{
    Init,
    Loading,
    Main,
    Battle,
    Change,
}

public enum Sound
{
    Click,
    Fail,
    Success,
    Move,
    Attack,
    Attacked,
    Chest,
    Strength,//强化装备
    GetReward,//获得奖励
    Treasure,//抽奖
    SkillIce,//冰封术
    SkillFire,//烈火
    SkillDark,//魔力汲取
    SkillCommon_1,//荆棘之甲
    SkillCommon_2,//魔法力场
    SkillCommon_3,//魔力膨胀
    SkillCommon_4,//全力一击
}

public enum Character
{
    Soldier,
    Archer,
    Master,
}

public enum Property
{
    Hp,
    Atk,
    Prop,
    Helmet,
    Corselet,   
    Cuish,  
}

public enum LevelType
{
    Normal,
    Endless,
}

public enum CardType
{
    Equipment,
    CommonBuff,
    ExclusiveBuff
}

public enum CommonBuffTriggerType
{
    Init,
    Attack,
    Attacked,
}

public enum CommonBuffSelectorType
{
    CurPos,
}

public enum CommonBuffTargetTag
{
    Character,
    Monster,
}

public enum CommonBuffEffectType
{
    AddAtk,
    DropCoin,
    Poison,
    Freeze,
    Kill,
    Restore,
}

public enum SoldierBuffTriggerType
{
    Init,
}

public enum SoldierBuffSelectorType
{
    CurPos,
}

public enum SoldierBuffTargetTag
{
    Character,
}

public enum SoldierBuffEffectType
{
    Restore,
    AddHpLimit
}

public enum ArcherBuffTriggerType
{
    Init,
    Miss,
}

public enum ArcherBuffSelectorType
{
    CurPos,
}

public enum ArcherBuffTargetTag
{
    Character,
}

public enum ArcherBuffEffectType
{
    AddMpLimit,
    AddAtk,
}

public enum MasterBuffTriggerType
{
    Init,
}

public enum MasterBuffSelectorType
{
    CurPos,
    All,
}

public enum MasterBuffTargetTag
{
    Character,
    Buff
}

public enum MasterBuffEffectType
{
    AddTime,
    RestoreMp
}

public enum SkillPointType
{
    Atk,
    Hp,
    Crit,
    AddDamage,
    Dodge,
    DamageReduction,
    Skill_1,
    Skill_2,
    Skill_3,
    Skill_4,
}

public class LoginParam
{
    public string adult_level;
    public string is_holiday;
    public string user_id;
    public string nickname;
    public string timestamp;
}
