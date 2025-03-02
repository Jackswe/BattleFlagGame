using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ������������
public class ControllerManager
{
    private Dictionary<int, BaseController> _modules;  // �洢���������ֵ�

    public ControllerManager() { 
        _modules = new Dictionary<int, BaseController>();
    }



    public void Register(ControllerType type, BaseController ctl) {
        Register((int)type,ctl);
    }


    // ע�������
    public void Register(int controllerKey, BaseController baseController) { 
        if(_modules.ContainsKey(controllerKey) == false)
        {
            // �ֵ��в�������������������
            _modules.Add(controllerKey, baseController);
        }
    }


    // ִ�����п�����Init����
    public void InitAddModules() {
        foreach (var item in _modules) {
            item.Value.Init();  // ����ִ�����п������еĳ�ʼ������
        }
    }



    // �Ƴ�������
    public void UnRegister(int controllerKey) { 
        if(_modules.ContainsKey(controllerKey) )
        {
            _modules.Remove(controllerKey);
        }
    }


    public void Clear() { 
        _modules.Clear();
    }

    // ������еĿ�����
    public void ClearAllModules() {
        List<int> keys = _modules.Keys.ToList();
        for(int i = 0; i< keys.Count; i++)
        {
            _modules[keys[i]].Destroy();
            _modules.Remove(keys[i]);
        }
    }


    // ��ģ�崥����Ϣ  ����key������Ӧ����Ϣ
    public void ApplyFunc(int controllerKey,string eventName, System.Object[] args) {
        if (_modules.ContainsKey(controllerKey)) {
            _modules[controllerKey].ApplyFunc(eventName, args);
        }
    }

    // ��ȡĳ��������Model����
    public BaseModel GetControllerModel(int controllerKey) {
        if (_modules.ContainsKey(controllerKey))
        {
            return _modules[controllerKey].GetModel();
        }
        else {
            // û���ҵ�
            return null;
        }
    }
}
