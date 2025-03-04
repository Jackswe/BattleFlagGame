using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// 加载场景控制器
public class LoadingController : BaseController
{
    AsyncOperation asyncOp;


    public LoadingController() : base() {
        //
        GameApp.ViewManager.Register(ViewType.LoadingView,new ViewInfo() {
            PrefabName = "LoadingView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf
        });

        InitModuleEvent();

    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.LoadingScene, loadSceneCallBack);  
    }

    // 加载场景回调
    private void loadSceneCallBack(System.Object[] args) { 
        LoadingModel loadingModel = args[0] as LoadingModel;

        SetModel(loadingModel);
        // 打开加载界面
        GameApp.ViewManager.Open(ViewType.LoadingView);
        // 加载场景
        asyncOp = SceneManager.LoadSceneAsync(loadingModel.SceneName);

        asyncOp.completed += onLoadedEndCallBack;

    }

    // 加载后回调函数
    private void onLoadedEndCallBack(AsyncOperation op) { 
        asyncOp.completed -= onLoadedEndCallBack;
        // 延迟一点
        GameApp.TimerManager.Register(0.25f, () =>
        {
            // 将加载页面延迟一点点
            GetModel<LoadingModel>().callback?.Invoke();  // 执行回调函数

            GameApp.ViewManager.Close((int)ViewType.LoadingView);  // 关闭加载界面  

        });


        GetModel<LoadingModel>().callback?.Invoke();  // 执行回调函数

        GameApp.ViewManager.Close((int)ViewType.LoadingView);  // 关闭加载界面

    }



}
