using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �ؿ�������
public class LevelController : BaseController
{
    public LevelController() : base() {

        SetModel(new LevelModel());  // ��������ģ��


        GameApp.ViewManager.Register(ViewType.SelectLevelView,new ViewInfo() { 
            PrefabName = "SelectLevelView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf
        });


        InitModuleEvent();
        InitGlobalEvent();
    }

    public override void Init()
    {
        model.Init();  // ��ʼ������
    }



    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenSelectLevelView, onOpenSelectLevelView);
    }


    // ע��ȫ���¼�
    public override void InitGlobalEvent()
    {
        GameApp.MessageCenter.AddEvent(Defines.ShowLevelDesEvent, onShowLevelDesCallBack);
        GameApp.MessageCenter.AddEvent(Defines.HideLevelDesEvent, onHideLevelDesCallBack);
    }

    // �Ƴ�ȫ���¼�
    public override void RemoveGlobalEvent()
    {
        GameApp.MessageCenter.RemoveEvent(Defines.ShowLevelDesEvent, onShowLevelDesCallBack);
        GameApp.MessageCenter.RemoveEvent(Defines.HideLevelDesEvent, onHideLevelDesCallBack);

    }


    private void onShowLevelDesCallBack(System.Object arg) {
        Debug.Log("levelId:" + arg.ToString());

        LevelModel levelMode = GetModel<LevelModel>();
        // ��ȡ�ؿ��ľ�����Ϣ
        levelMode.current = levelMode.GetLevel(int.Parse(arg.ToString()));

        GameApp.ViewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView).ShowLevelDes();
    }

    private void onHideLevelDesCallBack(System.Object arg) { 
        GameApp.ViewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView).HideLevelDes();

    }


    private void onOpenSelectLevelView(System.Object[] arg) {
        GameApp.ViewManager.Open(ViewType.SelectLevelView,arg);
    }



}
