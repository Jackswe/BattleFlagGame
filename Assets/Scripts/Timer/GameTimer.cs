using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
public class GameTimer
{
    private List<GameTimerData> timers;

    public GameTimer() { 
        timers = new List<GameTimerData>(); 
    }

    // 注册计时器
    public void Register(float timer,System.Action callback) { 
        GameTimerData data = new GameTimerData(timer,callback);
        timers.Add(data);
    }


    public void OnUpdate(float dt) {

        for (int i = timers.Count - 1; i >= 0; i--) {
            if (timers[i].OnUpdate(dt) == true) {
                timers.RemoveAt(i);
            }
        }
    }

    // 打断计时器
    public void Break() {
        timers.Clear();
    }

    public int Count() {
        return timers.Count;
    }
}
