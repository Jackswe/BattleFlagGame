using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageInfo {
    public string MsgTxt;
    public System.Action okCallBack;
    public System.Action noCallBack;
}

// 提示面板
public class MessageView : BaseView
{
    MessageInfo info;

    protected override void OnAwake()
    {
        base.OnAwake();

        Find<Button>("okBtn").onClick.AddListener(onOkBtn);
        Find<Button>("noBtn").onClick.AddListener(onNoBtn);

    }

    // 当界面打开的时候执行的函数
    public override void Open(params object[] args)
    {
        info = args[0] as MessageInfo;
        Find<Text>("content/txt").text = info.MsgTxt;
    }

    private void onOkBtn() {
        info.okCallBack?.Invoke();
    }

    private void onNoBtn() {
        info.noCallBack?.Invoke();
        GameApp.ViewManager.Close(ViewId);
    }
}
