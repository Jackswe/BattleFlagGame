using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// ��Ϸ��������  ������ʼ��Ϸ ���� �˳� �Ȳ�����
public class GameController : BaseController
{

    public GameController() : base() { 
        // Ŀǰû����ͼ



        InitModuleEvent();
        InitGlobalEvent();
    }

    public override void Init()
    {
        // ����GameUIController ��ʼ����¼�
        ApplyControllerFunc(ControllerType.GameUI,Defines.OpenStartView);   

    }
}
