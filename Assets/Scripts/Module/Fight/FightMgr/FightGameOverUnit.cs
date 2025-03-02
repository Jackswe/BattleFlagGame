using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 战斗结束
public class FightGameOverUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();

        GameApp.CommandManager.Clear(); // 清除指令

        if (GameApp.FightManager.heros.Count == 0)
        {
            // 显示界面
            GameApp.CommandManager.AddCommand(new WaitCommand(1.25f, () =>
            {
                GameApp.ViewManager.Open(ViewType.LossView);
            }));
        }
        else if (GameApp.FightManager.enemys.Count == 0)
        {
            // 显示界面
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
