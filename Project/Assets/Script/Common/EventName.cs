public static class EventName
{
    public const string ChangeScene = nameof(ChangeScene);
    public const string PlaySound = nameof(PlaySound);
    public const string PlayAnotherSound = nameof(PlayAnotherSound);
    public const string AttackCommand = nameof(AttackCommand);
    public const string ShieldCommand = nameof(ShieldCommand);
    public const string CharacterMove = nameof(CharacterMove);
    public const string CharacterMoveEnd = nameof(CharacterMoveEnd);
    public const string CharacterAttack = nameof(CharacterAttack);
    public const string CharacterHpChange = nameof(CharacterHpChange);
    public const string CharacterCurrentHpChange = nameof(CharacterCurrentHpChange);
    public const string CharacterMpChange = nameof(CharacterMpChange);
    public const string CharacterExpChange = nameof(CharacterExpChange);
    public const string CharacterAttacked = nameof(CharacterAttacked);
    public const string CharacterResurge = nameof(CharacterResurge);
    public const string MonsterAttack = nameof(MonsterAttack);
    public const string PassTile = nameof(PassTile);
    public const string CreateCardOption = nameof(CreateCardOption);
    public const string SetBuff = nameof(SetBuff);
    public const string GetCoin = nameof(GetCoin);
    public const string GetExp = nameof(GetExp);
    public const string GetSkillPoint = nameof(GetSkillPoint);
    public const string BuffInit = nameof(BuffInit);
    public const string RestoreHp = nameof(RestoreHp);
    public const string Poison = nameof(Poison);
    public const string Miss = nameof(Miss);
    public const string AddBuffTime = nameof(AddBuffTime);
    public const string Pass = nameof(Pass);
    public const string InitMonster = nameof(InitMonster);
    public const string MonsterDead = nameof(MonsterDead);
    public const string StopPlayerAttack = nameof(StopPlayerAttack);
    public const string StartPlayerAttack = nameof(StartPlayerAttack);
    public const string InsFriendAdd = nameof(InsFriendAdd);
    public const string UpdateFriendOnline = nameof(UpdateFriendOnline);
    public const string SelectFriend = nameof(SelectFriend);
    public const string CommonButtonClick = nameof(CommonButtonClick);
    public const string ChangePauseButtonSprites = nameof(ChangePauseButtonSprites);// 改变暂停按钮的图标
    public const string AutoBattle = nameof(AutoBattle); // 自动战斗
    public const string IceSkill = nameof(IceSkill);// 冰魔法师技能
    public const string FireSkill = nameof(FireSkill); // 火魔法师技能
    public const string DarkSkill = nameof(DarkSkill); // 暗魔法师技能
    public const string CommonSkill_1 = nameof(CommonSkill_1); // 荆棘之甲
    public const string CommonSkill_2 = nameof(CommonSkill_2); // 魔法力场
    public const string CommonSkill_3 = nameof(CommonSkill_3); // 魔力膨胀
    public const string CommonSkill_4 = nameof(CommonSkill_4); // 全力一击
    public const string SkillCountDown = nameof(SkillCountDown); // 技能倒计时
    public const string CharacterExtraAtkChange = nameof(CharacterExtraAtkChange); // 角色额外攻击力变化
    public const string EndCommonSkill_2 = nameof(EndCommonSkill_2); // 结束魔法力场技能
    public const string EndCommonSkill_3 = nameof(EndCommonSkill_3); // 结束魔力膨胀技能
    public const string EndCommonSkill_4 = nameof(EndCommonSkill_4); // 结束全力一击技能
    public const string ShopEvent = nameof(ShopEvent);
    public const string RefreshShop = nameof(RefreshShop);
    //弹窗事件
    public const string ShowCommonAward = nameof(ShowCommonAward);
    public const string ShowSkill = nameof(ShowSkill);
    public const string ShowEquip = nameof(ShowEquip);
    public const string ShowTreasure = nameof(ShowTreasure);
    public const string ShowCreateAccount = nameof(ShowCreateAccount);
    //Tips事件
    public const string ShowCommonTips = nameof(ShowCommonTips);

    public const string RefreshEquip = nameof(RefreshEquip);
    public const string RefreshStrength = nameof(RefreshStrength);
    public const string RefreshSlot = nameof(RefreshSlot);
    public const string RefreshCharacters = nameof(RefreshCharacters);
    public const string UpdateSkillTree = nameof(UpdateSkillTree);
    public const string UpdateChallengeDamage = nameof(UpdateChallengeDamage);
    public const string RefreshAFK = nameof(RefreshAFK);
    public const string RefreshTopMessage = nameof(RefreshTopMessage);
    
    //新手引导
    public const string GuidanceStepComplete = nameof(GuidanceStepComplete);
}
