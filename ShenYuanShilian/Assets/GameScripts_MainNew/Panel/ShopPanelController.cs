using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CostType
{
   Free,
   Coin,
   Diamond,
   Cash
}

public enum LimitType
{
   Daily,
   Weekly,
   Limitless,
}

public class ShopPanelController : MonoBehaviour
{
   public GameObject shopItemObj;
   public Transform dailyParent;
   public Transform weeklyParent;
   public Transform limitlessParent;
   private bool firstOpen = true;

   private void Start()
   {
      EventManager.Instance.AddListener(EventName.RefreshShop,RefreshShop);
   }

   private void OnEnable()
   {
      if (firstOpen)
      {
         firstOpen = false;
         Init();
      }
      else
      {
         RefreshShop(null, null);
      }
   }

   private void OnDestroy()
   {
      EventManager.Instance.RemoveListener(EventName.RefreshShop,RefreshShop);
   }

   private void RefreshShop(object sender, EventArgs e)
   {
      for (int i = 0; i < dailyParent.childCount; i++)
      {
         dailyParent.GetChild(i).GetComponent<ShopItem>().RefreshInfo();
      }
      for (int i = 0; i < weeklyParent.childCount; i++)
      {
         weeklyParent.GetChild(i).GetComponent<ShopItem>().RefreshInfo();
      }
      for (int i = 0; i < limitlessParent.childCount; i++)
      {
         limitlessParent.GetChild(i).GetComponent<ShopItem>().RefreshInfo();
      }
   }

   public void Init()
   {
      Dictionary<LimitType, Transform> parentDic = new Dictionary<LimitType, Transform>()
      {
         { LimitType.Daily, dailyParent },
         { LimitType.Weekly, weeklyParent },
         { LimitType.Limitless, limitlessParent },
      };
      foreach (var shopItemInfo in DataManager.Instance.shopItemList)
      {
         Enum.TryParse(shopItemInfo.limitType, out LimitType limitType);
         ShopItem item = MainObjectPool.Instance.SpawnFromPool("ShopItem", Vector3.zero, Quaternion.identity).GetComponent<ShopItem>();
         item.transform.SetParent(parentDic[limitType]);
         item.transform.localPosition = Vector3.zero;
         item.transform.localScale = Vector3.one;
         item.Init(shopItemInfo);
      }
   }

}
