using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPayButton : View
{
    public override string Name { get; }
    public override void HandleEvent(object data = null)
    {
        throw new NotImplementedException();
    }
    private void Awake()
    {
        gameObject.SetActive(!GetModel<GameModel>().FirstPay);
    }

    
}
