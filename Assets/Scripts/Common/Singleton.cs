using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;


// ������
public class Singleton<T>
{
    private static readonly T instance = Activator.CreateInstance<T>();

    public static T Instance => instance;

    // ��ʼ������
    public virtual void Init() { 
        
    }

    // ÿִ֡�з���
    public virtual void Update(float dt) { 
        
    }

    // �����ͷ�
    public virtual void onDestroy() { 
        
    }

}
