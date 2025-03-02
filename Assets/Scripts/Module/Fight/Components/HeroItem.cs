using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


// ������קӢ��ͼ��Ľű�
public class HeroItem : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    Dictionary<string, string> data;

    // Start is called before the first frame update
    void Start()
    {
        // ����Ӣ��ͼ��
        transform.Find("icon").GetComponent<Image>().SetIcon(data["Icon"]);
    }


    public void Init(Dictionary<string, string> data)
    {
        this.data = data;   
    }

    // ��ʼ��ק
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameApp.ViewManager.Open(ViewType.DragHeroView, data["Icon"]);
    }

    // ������ק
    public void OnEndDrag(PointerEventData eventData)
    {

        GameApp.ViewManager.Close((int)ViewType.DragHeroView);
        // �����ק���λ���Ƿ���block�ű�
        Tools.ScreenPointToRay2D(eventData.pressEventCamera, eventData.position, (Collider2D col) => { 
            if(col != null)
            {
                // �ص�������Ϊ�� ���ȡ���block
                Block b = col.GetComponent<Block>();
                if (b != null) {
                    // �з���
                    Debug.Log(b);
                    Destroy(gameObject);  // ɾ���������
                    // ����Ӣ������
                    GameApp.FightManager.AddHero(b,data);
                }
            }
        });
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
