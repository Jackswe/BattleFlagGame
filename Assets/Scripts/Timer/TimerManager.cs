using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �򵥵�һ��ȫ��ʱ���ʱ��
public class TimerManager
{
    GameTimer timer;

    public TimerManager() { 
        timer = new GameTimer();
    }

    // ע���ʱ��
    public void Register(float time,System.Action callback) { 
        timer.Register(time, callback);
    }

    public void OnUpdate(float dt) { 
        timer.OnUpdate(dt);
    }



}
