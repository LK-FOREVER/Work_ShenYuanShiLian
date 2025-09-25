using System;

public static class Consts
{
    #region Model
    public const string M_GameModel = nameof(M_GameModel);
    public const string M_CharacterModel = nameof(M_CharacterModel);
    #endregion

    #region View
    public const string V_LoadingScene = nameof(V_LoadingScene);
    public const string V_MainScene = nameof(V_MainScene);
    public const string V_BattleScene = nameof(V_BattleScene);
    public const string V_UpgradationView = nameof(V_UpgradationView);
    public const string V_PropertyView = nameof(V_PropertyView);
    public const string V_CharacterView = nameof(V_CharacterView);
    public const string V_PurchaseTipView = nameof(V_PurchaseTipView);
    public const string V_BuyTipView = nameof(V_BuyTipView);
    public const string V_MapView = nameof(V_MapView);
    public const string V_SkillTreeView = nameof(V_SkillTreeView);
    public const string V_SkillUpgradePanel = nameof(V_SkillUpgradePanel);
    public const string V_SkillUnlockPanel = nameof(V_SkillUnlockPanel);
    public const string V_Music = nameof(V_Music);
    public const string V_DeadBoard = nameof(V_DeadBoard);
    public const string V_SuccessBoard = nameof(V_SuccessBoard);
    public const string V_CardOptionBoard = nameof(V_CardOptionBoard);
    public const string V_MainSceneGuidanceBoard = nameof(V_MainSceneGuidanceBoard);
    public const string V_BattleSceneGuidanceBoard = nameof(V_BattleSceneGuidanceBoard);
    public const string V_SettingBoard = nameof(V_SettingBoard);
    public const string V_ShopBoard = nameof(V_ShopBoard);
    public const string V_ChallengeBoard = nameof(V_ChallengeBoard);
    public const string V_CommondGuidance = nameof(V_CommondGuidance);
    public const string V_ChallengeEndBoard = nameof(V_ChallengeEndBoard);

    //好友系统
    public const string V_FriendPopupView = nameof(V_FriendPopupView);
    public const string V_FriendContent_View = nameof(V_FriendContent_View);
    public const string FriendList_ContentView = nameof(FriendList_ContentView);
    public const string FriendApply_ContentView = nameof(FriendApply_ContentView);
    public const string FriendAdd_ContentView = nameof(FriendAdd_ContentView);
    public const string FriendList_View = nameof(FriendList_View);
    public const string FriendApply_View = nameof(FriendApply_View);
    public const string FriendAdd_View = nameof(FriendAdd_View);
    
    //奖励
    public const string CommonAwardPopupView = nameof(CommonAwardPopupView);
    public const string E_UpdateHp = nameof(E_UpdateHp);
    #endregion

    #region EventName
    public const string E_ChangeVolume = nameof(E_ChangeVolume);
    public const string E_ChangeCharacterModel = nameof(E_ChangeCharacterModel);
    public const string E_UnlockCharacter = nameof(E_UnlockCharacter);
    public const string E_ChooseCharacter = nameof(E_ChooseCharacter);
    public const string E_Upgrade = nameof(E_Upgrade);
    public const string E_ChangeCoin = nameof(E_ChangeCoin);
    public const string E_ChangeExp = nameof(E_ChangeExp);
    public const string E_UpdateCoin = nameof(E_UpdateCoin);
    public const string E_UpdateDiamond = nameof(E_UpdateDiamond);
    public const string E_UpdateExp = nameof(E_UpdateExp);
    public const string E_ChangeCurLevel = nameof(E_ChangeCurLevel);
    public const string E_SetEquipment = nameof(E_SetEquipment);
    public const string E_UpdateEquipment = nameof(E_UpdateEquipment);
    public const string E_SaveData = nameof(E_SaveData);
    //好友系统
    public const string E_UpdateFriends = nameof(E_UpdateFriends);
    #endregion
}
