using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBase : MonoBehaviour
{
    public int Id;  // ����id
    public Dictionary<string, string> data;  // ���ݱ�
    public int Step;   // �ж���
    public int Attack;  // ������
    public int Type;  // ����
    public int MaxHp;  // ���Ѫ��
    public int CurHp;  // ��ǰѪ��

    public int RowIndex;
    public int ColIndex;
    public SpriteRenderer bodySp;  // ����ͼƬ��Ⱦ���
    public GameObject stopObj;      // ֹͣ�ж��ı������

    public Animator animator; // �������


    private bool _isStop;  // �Ƿ��ƶ�����

    public bool IsStop { 
        get { 
            return _isStop; 
        }
        set { 
            stopObj.SetActive(value);
            if (value == true)
            {
                bodySp.color = Color.gray;
            }
            else {
                bodySp.color = Color.white;
            }
            _isStop = value;    
        }
    }

    private void Awake()
    {
        // ��ȡ���
        bodySp = transform.Find("body").GetComponent<SpriteRenderer>();
        stopObj = transform.Find("stop").gameObject;
        animator = transform.Find("body").GetComponent<Animator>();


    }

    protected virtual void Start()
    {
        AddEvents();
    }

    protected virtual void onDestroy() {
        RemoveEvents();
    }

    // ����¼�
    protected virtual void AddEvents() {
        GameApp.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.AddEvent(Defines.OnUnSelectEvent,OnUnSelectCallBack);
    }

    // �Ƴ��¼�
    protected virtual void RemoveEvents()
    {
        GameApp.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.RemoveEvent(Defines.OnUnSelectEvent,OnUnSelectCallBack);


    }

    // ѡ�лص�
    protected virtual void OnSelectCallBack(System.Object arg) {
        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
        // test
        GameApp.MapManager.ShowStepGrid(this,Step);
    }

    // δѡ�лص�
    protected virtual void OnUnSelectCallBack(System.Object arg)
    {
        GameApp.MapManager.HideStepGrid(this, Step);

    }


    // ת��
    public void Flip() { 
        Vector3 scale = transform.localScale;
        scale.x *= -1;

        transform.localScale = scale;
    }


    // �ƶ� ��ָ���±�ĸ���
    public virtual bool Move(int rowIndex, int colIndex,float dt) {
        // ��ȡĿ����ӵ�λ��
        Vector3 pos = GameApp.MapManager.GetBlockPos(rowIndex,colIndex);
        pos.z = transform.position.z;

        if (transform.position.x > pos.x && transform.localScale.x > 0) {
            // ת��
            Flip();
        }
        if(transform.position.x < pos.x  &&  transform.localScale.x < 0){
            // Ŀ��λ�õ�x ���ڵ�ǰλ�� ��ʾ���ұ��ƶ� ���ǽ�ɫ���������ʾ��Ҫת��
            Flip();
        }


        // �������Ŀ�ĵغܽ�  ����true   ��ʾ�ƶ�����ָ��λ��
        if (Vector3.Distance(transform.position, pos) <= 0.02f) { 
            this.RowIndex = rowIndex;
            this.ColIndex = colIndex;
            transform.position = pos;
            return true;
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, dt);

        return false;

    }

    // ���Ŷ���
    public void PlayAni(string aniName) {
        animator.Play(aniName);
    }


    // ����
    public virtual void GetHit(ISkill skill) { 
        
    }


    // ������Ч  ����Ч���壩
    public virtual void PlayEffect(string name) {
        GameObject obj = Instantiate(Resources.Load($"Effect/{name}")) as GameObject; 
        obj.transform.position = transform.position;
    }

    // ��������model�ľ���  ���������±����
    public float GetDis(ModelBase model) {
        return Mathf.Abs(RowIndex - model.RowIndex) + Mathf.Abs(ColIndex - model.ColIndex);
    }

    // ������Ч �������˵ȵ�
    public void PlaySound(string name) {
        GameApp.SoundManager.PlayEffect(name,transform.position);
    }

    public void LookAtModel(ModelBase model) {
        if ((model.transform.position.x > transform.position.x) && transform.localScale.x < 0)
        {
            Flip();
        } else if ((model.transform.position.x < transform.position.x) && transform.localScale.x > 0) {
            Flip();
        }
    }






}
