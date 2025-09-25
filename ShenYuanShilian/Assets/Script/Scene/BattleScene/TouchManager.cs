using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    enum slideVector { nullVector, up, down, left, right };
    private Vector2 touchFirst = Vector2.zero;
    private Vector2 touchSecond = Vector2.zero;
    private slideVector currentVector = slideVector.nullVector;
    public float SlidingDistance = 20f;

    void OnGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            if (!IsPointerOverUIObject(gameObject)) return;

            touchFirst = Event.current.mousePosition;
        }
        if (Event.current.type == EventType.MouseUp)
        {
            if (!IsPointerOverUIObject(gameObject)) return;

            currentVector = slideVector.nullVector;
            touchSecond = Event.current.mousePosition;
            Vector2 slideDirection = touchFirst - touchSecond;
            float x = slideDirection.x;
            float y = slideDirection.y;

            if (y + SlidingDistance < x && y > -x - SlidingDistance)
            {

                currentVector = slideVector.left;
            }
            else if (y > x + SlidingDistance && y < -x - SlidingDistance)
            {
                this.TriggerEvent(EventName.ShieldCommand);
                currentVector = slideVector.right;
            }
            else if (y > x + SlidingDistance && y - SlidingDistance > -x)
            {
                currentVector = slideVector.up;
            }
            else if (y + SlidingDistance < x && y < -x - SlidingDistance)
            {
                currentVector = slideVector.down;
            } else if ( Mathf.Abs( x - y ) <= 0.1)
            {
                this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Attack });
                this.TriggerEvent(EventName.AttackCommand);
            }
        }
    }

    private bool IsPointerOverUIObject(GameObject target)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        for(int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject == target && i == 0)
            {
                return true;
            }
        }
        return false;
    }

    //public void OnClick()
    //{
    //    this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Attack });
    //    this.TriggerEvent(EventName.AttackCommand);
    //}
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class TouchManager : MonoBehaviour
//{
//    private Vector2 DeltaArea;       //��ά��������������

//    private bool BoolSecondClick;           //�Ƿ�Ϊ�ڶ��ε��
//    private float FloFirstTime = 0f;          //��һ�ε��ʱ��
//    private float FloSecondTime = 0f;         //�ڶ��ε��ʱ��

//    // Use this for initialization
//    void Start()
//    {
//        Debug.Log("��ʼ��");
//        //��ʼ����������ֵ
//        DeltaArea = Vector2.zero;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        Debug.Log(Input.touchCount);
//        /* ��ָ�뿪��Ļ */
//        //Input.touchCount�Ǿ�̬���α�������һֻ��ָ�Ӵ�����Ļʱ����1����ֻ��ָ����2���Դ����ơ�
//        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Ended))
//        {
//            DeltaArea = Vector2.zero;
//            //DoubleClickTips.text = "";          //�����ָ�뿪��Ļ��˫��Ч����ʧ
//        }

//        /* ʶ����ָ���� */
//        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Moved))
//        {
//            DeltaArea.x += Input.GetTouch(0).deltaPosition.x;           //���ϻ�ȡ��ָ����ʱx,y��ı仯������ֵ����������
//            DeltaArea.y += Input.GetTouch(0).deltaPosition.y;
//            if (DeltaArea.x > 150)
//            {
//                Debug.Log("�һ���");
//                this.TriggerEvent(EventName.ShieldCommand);
//            }
//            else if (DeltaArea.x < -150)
//            {
//                Debug.Log("����");
//            }

//            if (DeltaArea.y > 150)
//            {
//                Debug.Log("�ϻ���");
//            }
//            else if (DeltaArea.y < -150)
//            {
//                Debug.Log("�»���");
//            }
//        }


//        /* ��ָ˫��ʶ��*/
//        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Began))
//        {
//            Debug.Log("��ʼ");
//            FloSecondTime = Time.time;
//            if (FloSecondTime - FloFirstTime > 0.02F && FloSecondTime - FloFirstTime < 0.3F)
//            {//���ڶ��ε�����һ�ε����ʱ������0.02����0.3��֮��ʱ
//                Debug.Log("˫��");
//            }
//            else
//            {
//                Debug.Log("����");
//                this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Attack });
//                this.TriggerEvent(EventName.AttackCommand);
//            }
//            FloFirstTime = Time.time;       //��¼ʱ��

//        }
//    }
//}
