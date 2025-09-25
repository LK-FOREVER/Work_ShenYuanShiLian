using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendContent_View : View
{
   public override string Name
   {
      get
      {
         return Consts.V_FriendContent_View;
      }
   }
   public override void HandleEvent(object data = null)
   {
      
   }
   
   public GameObject[] friendView;
   
   public TextMeshProUGUI nameText;
   public TextMeshProUGUI levelText;

   public Image headIcon;
   public Sprite[] headIconSprites;

   public FriendList_ContentView friendListContentView;
   public FriendAdd_ContentView friendAddContentView;
   public FriendApply_ContentView friendApplyContentView;

   public FriendClass currentFriend;
   
   public FriendClass CurrentFriend
   {
      get => currentFriend;
      set
      {
         currentFriend = value;
         friendListContentView.currentFriend = value;
         friendAddContentView.currentFriend = value;
         friendApplyContentView.currentFriend = value;
      }
      
   }
   
   private void Start()
   {
      Init();
   }

   public void Init()
   {
      nameText.text = currentFriend.name;
      levelText.text = "等级"+currentFriend.level.ToString();
      headIcon.sprite = headIconSprites[currentFriend.headIconIndex];
   }
   
   public void ActiveView(int type)
   {
      foreach (var view in friendView)
      {
         view.SetActive(false);
      }
      friendView[type].SetActive(true);
   }
   
}
