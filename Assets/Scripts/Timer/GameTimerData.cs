using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimerData
{
    private float timer;   // 计时时间
    private System.Action callback;  // 时间到达的时候的回调函数

    public GameTimerData(float timer,System.Action callback) { 
        this.timer = timer;
        this.callback = callback;
    }


    // 帧更新
    public bool OnUpdate(float dt) {
        timer -= dt;
        if (timer <= 0) { 
            this.callback.Invoke();   // 计时器时间到的时候执行回调函数
            return true;  
        }

        return false;   


    }

}
