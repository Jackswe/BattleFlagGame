using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ��ק������ͼ�����
public class DragHeroView : BaseView
{
    public void Update()
    {
        // ��ק�и�������ƶ� ��ʾ��ʱ��Ž����ƶ�
        if (_canvas.enabled == false) {
            return;  // ��ק����û����ʾ���ܽ����ƶ�
        }
        // ��������л�����������
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = worldPos;  // 
    }

    public override void Open(params object[] args)
    {
        transform.GetComponent<Image>().SetIcon(args[0].ToString());
    }
}
