public class UpdateFriendController : Controller
{
    public override void Excuse(object data = null)
    {
        UpdateFriends args = data as UpdateFriends;
        GetModel<GameModel>().FriendList = args.friendList;
    }
}
