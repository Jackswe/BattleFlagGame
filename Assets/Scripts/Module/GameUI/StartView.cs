using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// 开始游戏界面
public class StartView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();
        // 添加按钮点击事件 设置回调
        Find<Button>("startBtn").onClick.AddListener(onStartGameBtn);
        Find<Button>("setBtn").onClick.AddListener(onSetBtn);
        Find<Button>("quitBtn").onClick.AddListener(onQuitBtn);
    }

    
    private void onStartGameBtn() {
        // 关闭开始界面
        GameApp.ViewManager.Close(ViewId);


        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "map";
        loadingModel.callback = () =>
        {
            // 打开选择关卡界面
            Controller.ApplyControllerFunc(ControllerType.Level,Defines.OpenSelectLevelView);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading,Defines.LoadingScene, loadingModel);
    
    }
    // 设置按钮
    private void onSetBtn() {
        ApplyFunc(Defines.OpenSetView);  // 显示设置界面
    }

    private void onQuitBtn() {
        Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenMessageView, new MessageInfo()
        {
            okCallBack = () =>
            {
                Application.Quit();
            },
            MsgTxt = "确定退出游戏吗？",
            

        });
    }
}
