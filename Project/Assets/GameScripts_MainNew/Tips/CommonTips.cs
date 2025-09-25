using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommonTips : MonoBehaviour
{
   public TextMeshProUGUI showText;

   public void Show(string content)
   {
      showText.text = content;
   }
}
