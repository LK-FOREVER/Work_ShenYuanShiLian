using System;
using System.Collections.Generic;

public class PlaySoundEventArgs : EventArgs
{
    public Sound index;
}

public class ChangeSceneEventArgs : EventArgs
{
    public Scene index;
}

public class CharacterMoveEventArgs : EventArgs
{
    public int pos;
}

public class CharacterAttackEventArgs : EventArgs
{
    public int pos;
    public int atk;
}

public class MonsterAttackEventArgs : EventArgs
{
    public int atk;
}

public class MonsterInitEventArgs : EventArgs
{
    public int monsterNum;
}

public class CardOptionEventArgs : EventArgs
{
    public CardType cardType;
    public int id;
}
public class ChangePauseButtonSpritesEventArgs : EventArgs
{
    public bool isPause; // 是否暂停
}
public class SetBuffEventArgs : EventArgs
{
    public string type;
    public int id;
}

public class GetCoinEventArgs : EventArgs
{
    public int coin;
}
public class GetExpEventArgs : EventArgs
{
    public int exp;
}

public class CharacterHpChangeEventArgs : EventArgs
{
    public int hp;
    public float percent;
}
public class CharacterCurrentHpChangeEventArgs : EventArgs
{
    public int currentHp;
}

public class CharacterMpChangeEventArgs : EventArgs
{
    public int mp;
    public float percent;
}
public class CharacterExpChangeEventArgs : EventArgs
{
    public int exp;
    public float percent;
}
public class CharacterExtraAtkChangeEventArgs : EventArgs
{
    public int extraAtk;
}

public class RestoreHpEventArgs : EventArgs
{
    public int percent;
}

public class PoisonEventArgs : EventArgs
{
    public int pos;
    public int percent;
}

public class AddBuffTimeEventArgs : EventArgs
{
    public int time;
}

public class CharacterMoveEndEventArgs : EventArgs
{
    public int id;
}

public class MonsterDeadEventArgs : EventArgs
{
    public int currentMonsterNum;
}
public class AutoBattleEventArgs : EventArgs
{
    public bool isAutoBattle; // 是否自动战斗
}
public class SkillEventArgs : EventArgs
{
    public string skillName; // 技能名称
    public int cooldown; // 技能冷却时间
    public int duration; // 技能持续时间
}
public class CommonClick : EventArgs
{
    public CommonButton CommonButton;
}


/// <summary>
/// 奖励设置
/// </summary>
public class SetAward :EventArgs
{
    public List<AwardInfo> awardList;
}

/// <summary>
/// 奖励设置
/// </summary>
public class SetShopArg :EventArgs
{
    public ShopItemInfo curShopItemInfo;
}




/// <summary>
/// 技能展示
/// </summary>
public class ShowSkill :EventArgs
{
    public string skillName;
}

/// <summary>
/// 技能展示
/// </summary>
public class ShowCommonTips :EventArgs
{
    public string tipsContent;
}

/// <summary>
/// 武器刷新
/// </summary>
public class RefreshEquipArgs :EventArgs
{
    public EquipType refreshType;
}

/// <summary>
/// 强化刷新
/// </summary>
public class RefreshStrengthArgs :EventArgs
{
    public EquipType refreshType;
}


/// <summary>
/// 武器展示
/// </summary>
public class ShowEquip :EventArgs
{
    public string equipName;
    public bool isWearing;
    public bool isOpenFromEquip;
}

public class AFKReward : EventArgs
{
    public int rewardCoin; 
    public int rewardStone; 
    public int rewardExp; 
    public int rewardDiamond; 
}

public class SelectFriend : EventArgs
{
    public FriendClass curFriend; 
}

public class FreezeEventArgs : EventArgs
{
    public int time; // 冰冻时间
}

public class ChangeStateArgs : EventArgs
{
    public int guidanceID;
    public bool changeScene;
}
public class ChallengeDamageArgs : EventArgs
{
    public int challengeDamage;
}