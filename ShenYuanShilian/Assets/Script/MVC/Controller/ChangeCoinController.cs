public class ChangeCoinController : Controller
{
    public override void Excuse(object data = null)
    {
        ChangeCoin args = data as ChangeCoin;
        GetModel<GameModel>().Coin = args.coin;
    }
}
