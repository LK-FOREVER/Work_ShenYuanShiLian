using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个是主界面的玩家属性升级功能
public class UpgradeController : Controller
{
    public override void Excuse(object data = null)
    {
        Upgrade args = data as Upgrade;
        GetModel<GameModel>().Coin -= args.cost;
        switch (args.type)
        {
            case Property.Hp:
                int[] hpArr = { GetModel<GameModel>().HpLevel[0], GetModel<GameModel>().HpLevel[1], GetModel<GameModel>().HpLevel[2] };
                hpArr[args.id]++;
                GetModel<GameModel>().HpLevel = (int[])hpArr.Clone();
                break;
            case Property.Atk:
                int[] atkArr = { GetModel<GameModel>().AtkLevel[0], GetModel<GameModel>().AtkLevel[1], GetModel<GameModel>().AtkLevel[2] };
                atkArr[args.id]++;
                GetModel<GameModel>().AtkLevel = (int[])atkArr.Clone();
                break;
            case Property.Prop:
                int[] propArr = { GetModel<GameModel>().PropLevel[0], GetModel<GameModel>().PropLevel[1], GetModel<GameModel>().PropLevel[2] };
                propArr[args.id]++;
                GetModel<GameModel>().PropLevel = (int[])propArr.Clone();
                break;
            case Property.Helmet:
                int[] helmetArr = { GetModel<GameModel>().HelmetLevel[0], GetModel<GameModel>().HelmetLevel[1], GetModel<GameModel>().HelmetLevel[2] };
                helmetArr[args.id]++;
                GetModel<GameModel>().HelmetLevel = (int[])helmetArr.Clone();
                break;
            case Property.Corselet:
                int[] corseletArr = { GetModel<GameModel>().CorseletLevel[0], GetModel<GameModel>().CorseletLevel[1], GetModel<GameModel>().CorseletLevel[2] };
                corseletArr[args.id]++;
                GetModel<GameModel>().CorseletLevel = (int[])corseletArr.Clone();
                break;
            case Property.Cuish:
                int[] cuishArr = { GetModel<GameModel>().CuishLevel[0], GetModel<GameModel>().CuishLevel[1], GetModel<GameModel>().CuishLevel[2] };
                cuishArr[args.id]++;
                GetModel<GameModel>().CuishLevel = (int[])cuishArr.Clone();
                break;
            default:
                break;
        }
    }
}
