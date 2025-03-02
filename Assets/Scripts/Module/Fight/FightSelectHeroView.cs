using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ѡ��Ӣ�����
public class FightSelectHeroView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("bottom/startBtn").onClick.AddListener(onFightBtn);
    }

    // ������Ӣ�ۿ�ʼ���뵽��һغ�
    private void onFightBtn()
    {
        // ���һ��Ӣ�۶�û��ѡ Ҫ��ʾ��� ѡ��  TODO��չ

        if (GameApp.FightManager.heros.Count == 0)
        {
            Debug.Log("û��ѡ��Ӣ��");
        }
        else {
            GameApp.ViewManager.Close(ViewId);   // �رյ�ǰѡ��Ӣ�۵Ľ���

            //�л�����ҵĻغ�
            GameApp.FightManager.ChangeState(GameState.Player);
        }
    }



    public override void Open(params object[] args)
    {
        base.Open(args);

        GameObject prefabObj = Find("bottom/grid/item");
        Transform girdTf = Find("bottom/grid").transform;

        for (int i = 0; i < GameApp.GameDataManager.heros.Count; i++) {
            Dictionary<string, string> data = GameApp.ConfigManager.GetConfigData("player").GetDataById(GameApp.GameDataManager.heros[i]);
            
            GameObject obj = Object.Instantiate(prefabObj,girdTf); // ʵ��������
            obj.SetActive(true);

            HeroItem item = obj.AddComponent<HeroItem>();
            item.Init(data);
        }

    }

}
