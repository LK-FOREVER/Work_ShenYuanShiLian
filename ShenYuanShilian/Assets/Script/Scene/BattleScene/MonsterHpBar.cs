using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    [SerializeField]
    private GameObject hpBar;
    [SerializeField]
    private Transform barPoint;

    private Transform camera;
    private Transform bar;
    private TMP_Text hp;

    private Monster monster;

    private void Start()
    {
        monster = GetComponent<Monster>();

        monster.UpdateHpBar += UpdateHp;

        camera = Camera.main.transform;
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                bar = Instantiate(hpBar, canvas.transform).transform;
                hp = bar.transform.Find("Text").GetComponent<TMP_Text>();
                hp.text = monster.Hp.ToString();
            }
        }
        EventManager.Instance.TriggerEvent(EventName.StartPlayerAttack);
    }

    private void UpdateHp(int curHp, int maxHp)
    {
        if (curHp <= 0)
        {
            if (bar != null)
            {
                Destroy(bar.gameObject);
            }
        }
        else
        {
            if (bar != null)
            {
                float percent = (float)curHp / maxHp;
                bar.GetComponent<Slider>().value = percent;
                hp.text = curHp.ToString();
            }
        }
    }

    private void LateUpdate()
    {
        if (!bar) return;
        bar.position = barPoint.position;
    }
}
