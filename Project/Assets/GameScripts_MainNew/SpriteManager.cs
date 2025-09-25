using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public Sprite[] Weapon;
    public Sprite[] Necklace;
    public Sprite[] Cloak;
    public Sprite[] Head;
    public Sprite[] Armor;
    public Sprite[] Legs;
    public Sprite[] Quality;
    public Sprite[] RandomEquip;
    public Sprite[] Reward;
    public Sprite[] ShopItem;
    public static SpriteManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    public Sprite GetEquipSprite(string thisType,int id)
    {
        Enum.TryParse(thisType, out EquipType equipType);
        switch (equipType)
        {
            case EquipType.Weapon:
                return Weapon[id - 1];
            case EquipType.Necklace:
                return Necklace[id - 1];
            case EquipType.Cloak:
                return Cloak[id - 1];
            case EquipType.Head:
                return Head[id - 1];
            case EquipType.Armor:
                return Armor[id - 1];
            case EquipType.Legs:
                return Legs[id - 1];
        }
        return null;
    }
    
    public Sprite GetQualitySprite(int quality)
    {
        return Quality[quality-1];
    }
    
    public Sprite GetRandomEquipSprite(int quality)
    {
        return RandomEquip[quality-1];
    }
    
    public Sprite GetRewardSprite(RewardType rewardType)
    {
        switch (rewardType)
        {
            case RewardType.Coin:
                return Reward[0];
            case RewardType.Diamond:
                return Reward[1];
            case RewardType.Exp:
                return Reward[2];
            case RewardType.Stone:
                return Reward[3];
        }
        return null;
    }

    public Sprite GetShopSprite(string itemName)
    {
        switch (itemName)
        {
            case "60钻石":
                return ShopItem[0];
            case "100强化石":
                return ShopItem[1];
            case "100金币":
                return ShopItem[2];
            case "300钻石":
                return ShopItem[3];
            case "500强化石":
                return ShopItem[4];
            case "680钻石":
                return ShopItem[5];
            case "1280钻石":
                return ShopItem[6];
            case "2000金币":
                return ShopItem[7];
            case "3280钻石":
                return ShopItem[8];
            case "5000金币":
                return ShopItem[9];
            case "1000强化石":
                return ShopItem[10];
            
        }
        return null;
    }
}
