using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendPopupView : View
{
    public override string Name
    {
        get
        {
            return Consts.V_FriendPopupView;
        }
    }

    public override void HandleEvent(object data = null)
    {
        
    }
    

    private void Start()
    {
        Init();
    }
    
    public CommonButtonController buttonController;

    public void Init()
    {
        buttonController.InitClick();
    }

}
