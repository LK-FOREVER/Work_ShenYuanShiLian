using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacterController : Controller
{
    public override void Excuse(object data = null)
    {
        ChooseCharacter args = data as ChooseCharacter;
        GetModel<GameModel>().CurId = args.id;
    }
}
