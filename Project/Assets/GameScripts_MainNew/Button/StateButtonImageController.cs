using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateButtonImageController : MonoBehaviour
{

    public Image buttonImage;
    public Sprite enableSprite;
    public Sprite disableSprite;

    public void ChangeState(bool enabled)
    {
        buttonImage.sprite = enabled ? enableSprite : disableSprite;
    }
}
