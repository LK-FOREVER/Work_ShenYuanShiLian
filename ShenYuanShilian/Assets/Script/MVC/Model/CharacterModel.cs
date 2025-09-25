using UnityEngine;

public class CharacterModel : Model
{
    public override string Name
    {
        get
        {
            return Consts.M_GameModel;
        }
    }

    private int id;
    private int hpLevel;
    private int atkLevel;
    private int propLevel;
    private bool unlock;

    public CharacterModel(int id, int hpLevel, int atkLevel, int propLevel, bool unlock)
    {
        Id = id;
        HpLevel = hpLevel;
        AtkLevel = atkLevel;
        PropLevel = propLevel;
        Unlock = unlock;
    }

    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
            SendEvent(Consts.E_ChangeCharacterModel);
            SendEvent(Consts.E_SaveData);
        }
    }
    public int HpLevel
    {
        get
        {
            return hpLevel;
        }
        set
        {
            hpLevel = value;
            SendEvent(Consts.E_ChangeCharacterModel);
            SendEvent(Consts.E_SaveData);
        }
    }
    public int AtkLevel
    {
        get
        {
            return atkLevel;
        }
        set
        {
            atkLevel = value;
            SendEvent(Consts.E_ChangeCharacterModel);
            SendEvent(Consts.E_SaveData);
        }
    }
    public int PropLevel
    {
        get
        {
            return propLevel;
        }
        set
        {
            propLevel = value;
            SendEvent(Consts.E_ChangeCharacterModel);
            SendEvent(Consts.E_SaveData);
        }
    }
    public bool Unlock
    {
        get
        {
            return unlock;
        }
        set
        {
            unlock = value;
            SendEvent(Consts.E_ChangeCharacterModel);
            SendEvent(Consts.E_SaveData);
        }
    }
}
