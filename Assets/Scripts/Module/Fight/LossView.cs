using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class LossView : BaseView
{
    protected override void OnStart()
    {
        base.OnStart();
        Find<Button>("bg/okBtn").onClick.AddListener(() => {
            // ж��ս���е���Դ
            GameApp.FightManager.ReLoadRes();
            GameApp.ViewManager.CloseAll();

            // �л�����
            LoadingModel load = new LoadingModel();
            load.SceneName = "map";
            load.callback = () =>
            {
                GameApp.SoundManager.PlayBGM("mapbgm");
                GameApp.ViewManager.Open(ViewType.SelectLevelView);  // �л���ѡ�ؽ�����
            };
            Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, load);
        });
    }
}
