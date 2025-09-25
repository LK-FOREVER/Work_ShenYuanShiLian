using UnityEngine;

public class ChangeExpController : Controller
{
    public override void Excuse(object data = null)
    {
        ChangeExp args = data as ChangeExp;
        GetModel<GameModel>().Exp = args.exp;
    }
}
