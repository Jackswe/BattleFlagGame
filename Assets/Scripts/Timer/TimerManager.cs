using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 简单的一个全局时间计时器
public class TimerManager
{
    GameTimer timer;

    public TimerManager() { 
        timer = new GameTimer();
    }

    // 注册计时器
    public void Register(float time,System.Action callback) { 
        timer.Register(time, callback);
    }

    public void OnUpdate(float dt) { 
        timer.OnUpdate(dt);
    }



}
