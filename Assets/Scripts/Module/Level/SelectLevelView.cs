using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 选择关卡信息界面
public class SelectLevelView : BaseView
{
    protected override void OnStart()
    {
        base.OnStart();
        Find<Button>("close").onClick.AddListener(onCloseBtn);
        Find<Button>("level/fightBtn").onClick.AddListener(onFightBtn);

    }

    // 关闭 返回开始界面
    private void onCloseBtn() {
        // 关闭开始界面
        GameApp.ViewManager.Close(ViewId);
        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "game";  // 返回主界面
        loadingModel.callback = () =>
        {
            // 打开开始界面
            Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);

    }

    // 显示关卡描述信息面板
    public void ShowLevelDes() {
        Find("level").SetActive(true);  // 将名称为level的组件激活
        LevelData current = Controller.GetModel<LevelModel>().current;
        Find<Text>("level/name/txt").text = current.Name;
        Find<Text>("level/des/txt").text = current.Des;
    }

    public void HideLevelDes() {
        Find("level").SetActive(false);  // 将名称为level的组件失活

    }

    // 切换到战斗场景
    private void onFightBtn() {
        // 关闭当前界面
        GameApp.ViewManager.Close(ViewId);
        // 摄像机重置位置
        GameApp.CameraManager.ResetPos();       


        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = Controller.GetModel<LevelModel>().current.SceneName;  // 跳转的战斗场景
        loadingModel.callback = () =>
        {
            // 加载成功后显示战斗界面
            Controller.ApplyControllerFunc(ControllerType.Fight,Defines.BeginFight);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);

    }




}
