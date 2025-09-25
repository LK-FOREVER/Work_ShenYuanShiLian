public class SetEquipmentController : Controller
{
    public override void Excuse(object data = null)
    {
        SetEquipment args = data as SetEquipment;
        int characterId = GetModel<GameModel>().CurId;
        EquipmentInfo equipmentInfo = DataManager.Instance.equipmentInfoDic[args.id];

        int[] newArray = new int[] { -1, -1, -1 };
        switch (characterId)
        {
            case 0:
                GetModel<GameModel>().SoldierEquipments.CopyTo(newArray, 0);
                newArray[equipmentInfo.type] = args.id;
                GetModel<GameModel>().SoldierEquipments = (int[])newArray.Clone();
                break;
            case 1:
                GetModel<GameModel>().ArcherEquipments.CopyTo(newArray, 0);
                newArray[equipmentInfo.type] = args.id;
                GetModel<GameModel>().ArcherEquipments = (int[])newArray.Clone();
                break;
            case 2:
                GetModel<GameModel>().MasterEquipments.CopyTo(newArray, 0);
                newArray[equipmentInfo.type] = args.id;
                GetModel<GameModel>().MasterEquipments = (int[])newArray.Clone();
                break;
            default: break;
        }
    }
}
