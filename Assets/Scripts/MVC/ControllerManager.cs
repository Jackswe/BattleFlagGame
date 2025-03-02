using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 控制器管理器
public class ControllerManager
{
    private Dictionary<int, BaseController> _modules;  // 存储控制器的字典

    public ControllerManager() { 
        _modules = new Dictionary<int, BaseController>();
    }



    public void Register(ControllerType type, BaseController ctl) {
        Register((int)type,ctl);
    }


    // 注册控制器
    public void Register(int controllerKey, BaseController baseController) { 
        if(_modules.ContainsKey(controllerKey) == false)
        {
            // 字典中不包含这个控制器则添加
            _modules.Add(controllerKey, baseController);
        }
    }


    // 执行所有控制器Init函数
    public void InitAddModules() {
        foreach (var item in _modules) {
            item.Value.Init();  // 依次执行其中控制器中的初始化函数
        }
    }



    // 移除控制器
    public void UnRegister(int controllerKey) { 
        if(_modules.ContainsKey(controllerKey) )
        {
            _modules.Remove(controllerKey);
        }
    }


    public void Clear() { 
        _modules.Clear();
    }

    // 清除所有的控制器
    public void ClearAllModules() {
        List<int> keys = _modules.Keys.ToList();
        for(int i = 0; i< keys.Count; i++)
        {
            _modules[keys[i]].Destroy();
            _modules.Remove(keys[i]);
        }
    }


    // 跨模板触发消息  根据key触发对应的消息
    public void ApplyFunc(int controllerKey,string eventName, System.Object[] args) {
        if (_modules.ContainsKey(controllerKey)) {
            _modules[controllerKey].ApplyFunc(eventName, args);
        }
    }

    // 获取某控制器的Model对象
    public BaseModel GetControllerModel(int controllerKey) {
        if (_modules.ContainsKey(controllerKey))
        {
            return _modules[controllerKey].GetModel();
        }
        else {
            // 没有找到
            return null;
        }
    }
}
