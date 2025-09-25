using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCurLevelController : Controller
{
    public override void Excuse(object data = null)
    {
        ChangeCurLevel args = data as ChangeCurLevel;
        GetModel<GameModel>().CurLevel = args.curLevel;
    }
}
