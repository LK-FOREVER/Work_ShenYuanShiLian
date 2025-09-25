using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CommonButton : MonoBehaviour
{
    [SerializeField] public Sprite normalImage;
    [SerializeField] public Sprite pressedImage;
    public GameObject activeObj;
    
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        this.GetComponent<Button>().onClick.RemoveListener(OnClick);
    }

    public virtual void OnClick()
    {
        if (pressedImage!=null)
        {
            this.GetComponent<Image>().sprite = pressedImage;
        }
        if (activeObj!=null)
        {
            activeObj.SetActive(true);
        }
        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Click });
        EventManager.Instance.TriggerEvent(EventName.CommonButtonClick,this,new CommonClick(){CommonButton = this.GetComponent<CommonButton>()});
    }

    public void Init()
    {
        if (normalImage!=null)
        {
            this.GetComponent<Image>().sprite = normalImage;
        }
        if (activeObj!=null)
        {
            activeObj.SetActive(false);
        }
    }

    public void ChangeState(bool isPressed)
    {
        this.GetComponent<Image>().sprite = isPressed ? pressedImage : normalImage;
    }
}