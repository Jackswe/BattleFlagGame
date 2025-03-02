using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBase : MonoBehaviour
{
    public int Id;  // 物体id
    public Dictionary<string, string> data;  // 数据表
    public int Step;   // 行动力
    public int Attack;  // 攻击力
    public int Type;  // 类型
    public int MaxHp;  // 最大血量
    public int CurHp;  // 当前血量

    public int RowIndex;
    public int ColIndex;
    public SpriteRenderer bodySp;  // 身体图片渲染组件
    public GameObject stopObj;      // 停止行动的标记物体

    public Animator animator; // 动画组件


    private bool _isStop;  // 是否移动完标记

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
        // 获取组件
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

    // 添加事件
    protected virtual void AddEvents() {
        GameApp.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.AddEvent(Defines.OnUnSelectEvent,OnUnSelectCallBack);
    }

    // 移除事件
    protected virtual void RemoveEvents()
    {
        GameApp.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.RemoveEvent(Defines.OnUnSelectEvent,OnUnSelectCallBack);


    }

    // 选中回调
    protected virtual void OnSelectCallBack(System.Object arg) {
        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
        // test
        GameApp.MapManager.ShowStepGrid(this,Step);
    }

    // 未选中回调
    protected virtual void OnUnSelectCallBack(System.Object arg)
    {
        GameApp.MapManager.HideStepGrid(this, Step);

    }


    // 转向
    public void Flip() { 
        Vector3 scale = transform.localScale;
        scale.x *= -1;

        transform.localScale = scale;
    }


    // 移动 到指定下标的格子
    public virtual bool Move(int rowIndex, int colIndex,float dt) {
        // 获取目标格子的位置
        Vector3 pos = GameApp.MapManager.GetBlockPos(rowIndex,colIndex);
        pos.z = transform.position.z;

        if (transform.position.x > pos.x && transform.localScale.x > 0) {
            // 转向
            Flip();
        }
        if(transform.position.x < pos.x  &&  transform.localScale.x < 0){
            // 目标位置的x 大于当前位置 表示向右边移动 但是角色方向向左表示需要转向
            Flip();
        }


        // 如果距离目的地很近  返回true   表示移动到了指定位置
        if (Vector3.Distance(transform.position, pos) <= 0.02f) { 
            this.RowIndex = rowIndex;
            this.ColIndex = colIndex;
            transform.position = pos;
            return true;
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, dt);

        return false;

    }

    // 播放动画
    public void PlayAni(string aniName) {
        animator.Play(aniName);
    }


    // 受伤
    public virtual void GetHit(ISkill skill) { 
        
    }


    // 播放特效  （特效物体）
    public virtual void PlayEffect(string name) {
        GameObject obj = Instantiate(Resources.Load($"Effect/{name}")) as GameObject; 
        obj.transform.position = transform.position;
    }

    // 计算两个model的距离  根据行列下标计算
    public float GetDis(ModelBase model) {
        return Mathf.Abs(RowIndex - model.RowIndex) + Mathf.Abs(ColIndex - model.ColIndex);
    }

    // 播放音效 攻击受伤等等
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
