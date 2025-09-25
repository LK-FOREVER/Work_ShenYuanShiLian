using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCharacterController : Controller
{
    public override void Excuse(object data = null)
    {
        UnlockCharacher args = data as UnlockCharacher;
        bool[] newArray = new bool[] { true, false, false };
        GetModel<GameModel>().Unlock.CopyTo(newArray, 0);
        newArray[args.id] = true;
        GetModel<GameModel>().Unlock = (bool[])newArray.Clone();
    }
}
