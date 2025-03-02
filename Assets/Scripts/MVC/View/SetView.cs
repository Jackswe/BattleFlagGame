using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ������� ��������
public class SetView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();

        // Ϊ����ϵĿؼ� ����¼�
        Find<Button>("bg/closeBtn").onClick.AddListener(onCloseBtn);
        Find<Toggle>("bg/IsOpnSound").onValueChanged.AddListener(onIsStopBtn);
        Find<Slider>("bg/soundCount").onValueChanged.AddListener(onSliderBgmBtn);
        Find<Slider>("bg/effectCount").onValueChanged.AddListener(onSliderSoundEffectBtn);

        // ����ʵ����ֵ��С
        Find<Toggle>("bg/IsOpnSound").isOn = GameApp.SoundManager.IsStop;
        Find<Slider>("bg/soundCount").value = GameApp.SoundManager.BgmVolume;
        Find<Slider>("bg/effectCount").value = GameApp.SoundManager.EffectVolume;

    }


    // �Ƿ���
    private void onIsStopBtn(bool isStop) { 
        GameApp.SoundManager.IsStop = isStop;
    }


    // ����bgm����
    private void onSliderBgmBtn(float value) { 
        GameApp.SoundManager.BgmVolume = value;
    }


    private void onSliderSoundEffectBtn(float value) { 
        GameApp.SoundManager.EffectVolume = value;
    }


    // �رհ�ť
    private void onCloseBtn() {
        GameApp.ViewManager.Close(ViewId);  // �ر��Լ�
    }
}
