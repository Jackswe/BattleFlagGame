using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ս������
public class FightGameOverUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();

        GameApp.CommandManager.Clear(); // ���ָ��

        if (GameApp.FightManager.heros.Count == 0)
        {
            // ��ʾ����
            GameApp.CommandManager.AddCommand(new WaitCommand(1.25f, () =>
            {
                GameApp.ViewManager.Open(ViewType.LossView);
            }));
        }
        else if (GameApp.FightManager.enemys.Count == 0)
        {
            // ��ʾ����
            GameApp.CommandManager.AddCommand(new WaitCommand(1.25f, () =>
            {
                GameApp.ViewManager.Open(ViewType.WinView);
            }));
        }
        else { 
            
        }
    }

    public override bool Update(float dt)
    {
        return true;
    }
}
