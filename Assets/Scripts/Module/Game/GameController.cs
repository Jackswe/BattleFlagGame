using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 游戏主控制器  （处理开始游戏 保存 退出 等操作）
public class GameController : BaseController
{

    public GameController() : base() { 
        // 目前没有视图



        InitModuleEvent();
        InitGlobalEvent();
    }

    public override void Init()
    {
        // 调用GameUIController 开始面板事件
        ApplyControllerFunc(ControllerType.GameUI,Defines.OpenStartView);   

    }
}
