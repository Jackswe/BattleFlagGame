using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 关卡控制器
public class LevelController : BaseController
{
    public LevelController() : base() {

        SetModel(new LevelModel());  // 设置数据模型


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
        model.Init();  // 初始化数据
    }



    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenSelectLevelView, onOpenSelectLevelView);
    }


    // 注册全局事件
    public override void InitGlobalEvent()
    {
        GameApp.MessageCenter.AddEvent(Defines.ShowLevelDesEvent, onShowLevelDesCallBack);
        GameApp.MessageCenter.AddEvent(Defines.HideLevelDesEvent, onHideLevelDesCallBack);
    }

    // 移除全局事件
    public override void RemoveGlobalEvent()
    {
        GameApp.MessageCenter.RemoveEvent(Defines.ShowLevelDesEvent, onShowLevelDesCallBack);
        GameApp.MessageCenter.RemoveEvent(Defines.HideLevelDesEvent, onHideLevelDesCallBack);

    }


    private void onShowLevelDesCallBack(System.Object arg) {
        Debug.Log("levelId:" + arg.ToString());

        LevelModel levelMode = GetModel<LevelModel>();
        // 获取关卡的具体信息
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
