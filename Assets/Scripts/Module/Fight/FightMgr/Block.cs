using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType { 
    Null,
    Obstacle,  // �ϰ���
}

// ��ͼ�ĵ�Ԫ����
public class Block : MonoBehaviour
{

    public int RowIndex;  // ���±�
    public int ColIndex;  // ���±�
    public BlockType Type;  // ��������
    private SpriteRenderer selectSp;  // ѡ�еĸ���ͼƬ
    private SpriteRenderer gridSp;      // ����ͼƬ
    private SpriteRenderer dirSp;       // �ƶ������ͼƬ

    private void Awake()
    {
        selectSp = transform.Find("select").GetComponent<SpriteRenderer>();
        gridSp = transform.Find("grid").GetComponent<SpriteRenderer>();
        dirSp = transform.Find("dir").GetComponent<SpriteRenderer>();

        GameApp.MessageCenter.AddEvent(gameObject,Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.AddEvent(Defines.OnUnSelectEvent, onUnSelectCallBack);
    }

    private void OnDestroy()
    {
        GameApp.MessageCenter.RemoveEvent(gameObject,Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.RemoveEvent(Defines.OnUnSelectEvent, onUnSelectCallBack);

    }

    // ��ʾ����
    public void ShowGrid(Color color) { 
        gridSp.enabled = true;
        gridSp.color = color;
    }


    // ���ظ���
    public void HideGrid() {
        gridSp.enabled = false;
    }

    void OnSelectCallBack(System.Object arg) {
        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
        if (GameApp.CommandManager.IsRunningCommand == false) {
            GameApp.ViewManager.Open(ViewType.FightOptionDesView);  
        }
    }

    // δѡ��
    void onUnSelectCallBack(System.Object arg) {
        dirSp.sprite = null;
        GameApp.ViewManager.Close((int)ViewType.FightOptionDesView);

    }


    private void OnMouseEnter()
    {
        selectSp.enabled = true;
    }


    private void OnMouseExit()
    {
        selectSp.enabled = false;  
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // ���ü�ͷ�����ͼƬ��Դ �� ��ɫ
    public void SetDirSp(Sprite sp,Color color) {
        dirSp.sprite = sp;
        dirSp.color = color;
    }

}
