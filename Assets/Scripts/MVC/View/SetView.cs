using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 设置面板 （音量）
public class SetView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();

        // 为面板上的控件 添加事件
        Find<Button>("bg/closeBtn").onClick.AddListener(onCloseBtn);
        Find<Toggle>("bg/IsOpnSound").onValueChanged.AddListener(onIsStopBtn);
        Find<Slider>("bg/soundCount").onValueChanged.AddListener(onSliderBgmBtn);
        Find<Slider>("bg/effectCount").onValueChanged.AddListener(onSliderSoundEffectBtn);

        // 调整实际数值大小
        Find<Toggle>("bg/IsOpnSound").isOn = GameApp.SoundManager.IsStop;
        Find<Slider>("bg/soundCount").value = GameApp.SoundManager.BgmVolume;
        Find<Slider>("bg/effectCount").value = GameApp.SoundManager.EffectVolume;

    }


    // 是否静音
    private void onIsStopBtn(bool isStop) { 
        GameApp.SoundManager.IsStop = isStop;
    }


    // 设置bgm音量
    private void onSliderBgmBtn(float value) { 
        GameApp.SoundManager.BgmVolume = value;
    }


    private void onSliderSoundEffectBtn(float value) { 
        GameApp.SoundManager.EffectVolume = value;
    }


    // 关闭按钮
    private void onCloseBtn() {
        GameApp.ViewManager.Close(ViewId);  // 关闭自己
    }
}
