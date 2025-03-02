using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


// 控制器基类
public class BaseController
{
    private Dictionary<string, System.Action<object[]>> message; // 事件字典

    protected BaseModel model; // 模版数据

    public BaseController() {
        message = new Dictionary<string, System.Action<object[]>>();
    }


    // 注册后调用的初始化函数  （要所有控制器初始化后执行，）
    public virtual void Init() { 
        
    }



    public virtual void OnLoadView(IBaseView view) { }  // 加载视图

    // 打开视图
    public virtual void OpenView(IBaseView view) { 
        
    }

    // 关闭视图
    public virtual void CLoseView(IBaseView view) { 
        
    }

    // 注册模版事件
    public void RegisterFunc(string eventName, System.Action<object[]> callback) {
        if (message.ContainsKey(eventName))
        {
            // 包含该事件
            message[eventName] += callback;
        }
        else { 
            message.Add(eventName, callback);
        }
    }

    public void UnRegisterFunc(string eventName) {
        if (message.ContainsKey(eventName)) { 
             // 如果包含这个 则删除
             message.Remove(eventName);
        }
    }


    // 触发本模块事件
    public void ApplyFunc(string eventName, params object[] args) {
        if (message.ContainsKey(eventName))
        {
            message[eventName].Invoke(args);

        }
        else {
            Debug.Log("error:" + eventName);
        }
    }

    // 触发其他模版的事件
    public void ApplyControllerFunc(int controllerKey, string eventName, params object[] args) { 
        GameApp.ControllerManager.ApplyFunc(controllerKey, eventName, args);

    }

    // 触发模版事件  重载
    public void ApplyControllerFunc(ControllerType type,string eventName,params object[] args) { 
        ApplyControllerFunc((int)type, eventName, args);
    }



    // 设置模型数据
    public void SetModel(BaseModel model) { 
        this.model = model;
        this.model.controller = this;
    }

    public BaseModel GetModel() {
        return model;
    }

    public T GetModel<T>() where T:BaseModel{
        return model as T;
    }

    public BaseModel GetControllerModel(int controllerKey) {
        // TODO
        return GameApp.ControllerManager.GetControllerModel(controllerKey);
    }

    // 删除控制器
    public virtual void Destroy() { 

        RemoveModuleEvent();
        RemoveGlobalEvent();
    }

    // 初始化模版事件
    public virtual void InitModuleEvent() { 
        
    }

    // 移除模版事件
    public virtual void RemoveModuleEvent() { 
        
    }

    // 初始化全局事件
    public virtual void InitGlobalEvent() { 
        
    }

    // 移除全局事件
    public virtual void RemoveGlobalEvent() { 
        
    }


}
