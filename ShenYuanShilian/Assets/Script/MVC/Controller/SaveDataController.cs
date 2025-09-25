using UnityEngine;

public class SaveDataController : Controller
{
    public override void Excuse(object data = null)
    {
        string gameData = FullSerializerAPI.Serialize(typeof(string), GetModel<GameModel>().GameData);
        PlayerPrefs.SetString(GetModel<GameModel>().Account, gameData);
        PlayerPrefs.Save();
    }
}
