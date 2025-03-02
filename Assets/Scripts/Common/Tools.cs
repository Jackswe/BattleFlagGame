using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ������
public static class Tools
{

    public static void SetIcon(this Image img, string res) {

        img.sprite = Resources.Load<Sprite>($"Icon/{res}");
    }


    // ������λ���Ƿ���2d��ײ����
    public static void ScreenPointToRay2D(Camera cam, Vector2 mousePos, System.Action<Collider2D> callback) { 
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);  // ��Ļ����ת��������

        // ʹ�� Physics2D.OverlapCircle �������� worldPos ��λ�ô���һ���뾶Ϊ 0.02 ��Բ��
        // ������Բ��Χ���Ƿ��� Collider2D ��ײ����ڡ�
        Collider2D col = Physics2D.OverlapCircle(worldPos, 0.02f);  
        // ���������ײ����ûص�����
        callback?.Invoke(col);
    }

    // ���������ײ��
    public static Collider2D ScreenPointToRay2D(Camera cam,Vector2 mousePos) {
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);  // ��Ļ����ת��������

        // ʹ�� Physics2D.OverlapCircle �������� worldPos ��λ�ô���һ���뾶Ϊ 0.02 ��Բ��
        // ������Բ��Χ���Ƿ��� Collider2D ��ײ����ڡ�
        Collider2D col = Physics2D.OverlapCircle(worldPos, 0.02f);

        return col;
    }




}
