using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimerData
{
    private float timer;   // ��ʱʱ��
    private System.Action callback;  // ʱ�䵽���ʱ��Ļص�����

    public GameTimerData(float timer,System.Action callback) { 
        this.timer = timer;
        this.callback = callback;
    }


    // ֡����
    public bool OnUpdate(float dt) {
        timer -= dt;
        if (timer <= 0) { 
            this.callback.Invoke();   // ��ʱ��ʱ�䵽��ʱ��ִ�лص�����
            return true;  
        }

        return false;   


    }

}
