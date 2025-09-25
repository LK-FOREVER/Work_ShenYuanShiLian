using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateButton : Button
{


    public bool Interactable
    {
        get
        {
            return interactable;
        }
        set
        {
            interactable = value;
            this.GetComponent<StateButtonImageController>().ChangeState(interactable);
        }
    }

}
