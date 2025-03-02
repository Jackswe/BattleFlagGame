using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ѡ��ؿ���Ϣ����
public class SelectLevelView : BaseView
{
    protected override void OnStart()
    {
        base.OnStart();
        Find<Button>("close").onClick.AddListener(onCloseBtn);
        Find<Button>("level/fightBtn").onClick.AddListener(onFightBtn);

    }

    // �ر� ���ؿ�ʼ����
    private void onCloseBtn() {
        // �رտ�ʼ����
        GameApp.ViewManager.Close(ViewId);
        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "game";  // ����������
        loadingModel.callback = () =>
        {
            // �򿪿�ʼ����
            Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);

    }

    // ��ʾ�ؿ�������Ϣ���
    public void ShowLevelDes() {
        Find("level").SetActive(true);  // ������Ϊlevel���������
        LevelData current = Controller.GetModel<LevelModel>().current;
        Find<Text>("level/name/txt").text = current.Name;
        Find<Text>("level/des/txt").text = current.Des;
    }

    public void HideLevelDes() {
        Find("level").SetActive(false);  // ������Ϊlevel�����ʧ��

    }

    // �л���ս������
    private void onFightBtn() {
        // �رյ�ǰ����
        GameApp.ViewManager.Close(ViewId);
        // ���������λ��
        GameApp.CameraManager.ResetPos();       


        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = Controller.GetModel<LevelModel>().current.SceneName;  // ��ת��ս������
        loadingModel.callback = () =>
        {
            // ���سɹ�����ʾս������
            Controller.ApplyControllerFunc(ControllerType.Fight,Defines.BeginFight);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);

    }




}
