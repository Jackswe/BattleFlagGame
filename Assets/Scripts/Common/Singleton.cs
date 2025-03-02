using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;


// 单例类
public class Singleton<T>
{
    private static readonly T instance = Activator.CreateInstance<T>();

    public static T Instance => instance;

    // 初始化方法
    public virtual void Init() { 
        
    }

    // 每帧执行方法
    public virtual void Update(float dt) { 
        
    }

    // 销毁释放
    public virtual void onDestroy() { 
        
    }

}
