using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// ��ʼ��Ϸ����
public class StartView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();
        // ��Ӱ�ť����¼� ���ûص�
        Find<Button>("startBtn").onClick.AddListener(onStartGameBtn);
        Find<Button>("setBtn").onClick.AddListener(onSetBtn);
        Find<Button>("quitBtn").onClick.AddListener(onQuitBtn);
    }

    
    private void onStartGameBtn() {
        // �رտ�ʼ����
        GameApp.ViewManager.Close(ViewId);


        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "map";
        loadingModel.callback = () =>
        {
            // ��ѡ��ؿ�����
            Controller.ApplyControllerFunc(ControllerType.Level,Defines.OpenSelectLevelView);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading,Defines.LoadingScene, loadingModel);
    
    }
    // ���ð�ť
    private void onSetBtn() {
        ApplyFunc(Defines.OpenSetView);  // ��ʾ���ý���
    }

    private void onQuitBtn() {
        Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenMessageView, new MessageInfo()
        {
            okCallBack = () =>
            {
                Application.Quit();
            },
            MsgTxt = "ȷ���˳���Ϸ��",
            

        });
    }
}
