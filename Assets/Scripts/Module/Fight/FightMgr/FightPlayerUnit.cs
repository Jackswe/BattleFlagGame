using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 玩家的回合
public class FightPlayerUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        GameApp.ViewManager.Open(ViewType.TipView,"玩家回合");
    }
}
