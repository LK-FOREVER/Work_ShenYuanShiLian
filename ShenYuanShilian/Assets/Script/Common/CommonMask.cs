using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CommonMask : MonoBehaviour
{
    private void Start()
    {
        if (GetComponent<Button>() == null)
        {
            this.AddComponent<Button>();
        }
        GetComponent<Button>().onClick.AddListener(HideParent);
    }


    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveListener(HideParent);
    }

    private void HideParent()
    {
        transform.parent.gameObject.SetActive(false);
    }



}
