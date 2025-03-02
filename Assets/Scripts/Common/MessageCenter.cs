using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 消息处理中心脚本
public class MessageCenter
{
    private Dictionary<string, System.Action<object>> msgDic;  // 存储普通的消息字典
    private Dictionary<string, System.Action<object>> tempMsgDic;  // 存储临时的消息字典，执行后移除
    private Dictionary<System.Object, Dictionary<string, System.Action<object>>> objMsgDic;  // 存储特定对象的消息

    public MessageCenter() {
        msgDic = new Dictionary<string, System.Action<object>>();
        tempMsgDic = new Dictionary<string, System.Action<object>>();
        objMsgDic = new Dictionary<object, Dictionary<string, System.Action<object>>>();
    }

    // 添加事件
    public void AddEvent(string eventName,System.Action<object> callback) {
        if (msgDic.ContainsKey(eventName))
        {
            msgDic[eventName] += callback;
        }
        else { 
            msgDic.Add(eventName, callback);
        }
    }


    // 移除事件
    public void RemoveEvent(string eventName,System.Action<object> callback)
    {
        if (msgDic.ContainsKey(eventName))
        {
            msgDic[eventName] -= callback;
            if (msgDic[eventName] == null)
            {
                msgDic.Remove(eventName);  // 如果这个事件集合为空 则直接删除
            }
        }
    }

    // 执行事件
    public void PostEvent(string eventName,object arg = null) { 
        if(msgDic.ContainsKey(eventName))
        {
            msgDic[eventName].Invoke(arg);
        }
    }

    // 添加对象事件
    public void AddEvent(System.Object listenerObj, string eventName, System.Action<object> callback) {
        if (objMsgDic.ContainsKey(listenerObj))
        {
            if (objMsgDic[listenerObj].ContainsKey(eventName))
            {
                objMsgDic[listenerObj][eventName] += callback;
            }
            else
            {
                objMsgDic[listenerObj].Add(eventName, callback);
            }
        }
        else { 
            // 表示整个集合都没有这个对象的事件集合
            Dictionary<string,System.Action<object>> _tempDic = new Dictionary<string, System.Action<object>>();
            _tempDic.Add(eventName, callback);
            objMsgDic.Add(listenerObj, _tempDic);  
        }
    }

    // 移除对象的事件集合中的事件
    public void RemoveEvent(System.Object listenerObj, string eventName,System.Action<object> callback) { 
        if(objMsgDic.ContainsKey(listenerObj))
        {
            if (objMsgDic[listenerObj].ContainsKey(eventName)) {
                objMsgDic[listenerObj][eventName] -= callback;
                if (objMsgDic[listenerObj][eventName] == null) {
                    // 删除之后集合为空 则直接删除这个集合
                    objMsgDic[listenerObj].Remove(eventName);
                    if (objMsgDic[listenerObj].Count == 0)
                    {
                        // 如果集合中对象的事件个数为0
                        objMsgDic.Remove(listenerObj);
                    }
                }
            }
        }
    }


    // 移除对象的所有事件
    public void RemoveObjAllEvent(System.Object listenerObj) {
        if (objMsgDic.ContainsKey(listenerObj)) {
            objMsgDic.Remove(listenerObj);
        }
    }

    // 执行对象的事件
    /// <param name="listenerObj">对象集合名称</param>
    /// <param name="eventName">事件名</param>
    /// <param name="arg">事件参数</param>
    public void PostEvent(System.Object listenerObj,string eventName,System.Object arg = null) {
        if (objMsgDic.ContainsKey(listenerObj)) {
            if (objMsgDic[listenerObj].ContainsKey(eventName)) {
                objMsgDic[listenerObj][eventName].Invoke(arg);
            }
        }
    }

    // 添加临时事件 要覆盖
    public void AddTempEvent(string eventName,System.Action<object> callback) {
        if (tempMsgDic.ContainsKey(eventName))
        {
            tempMsgDic[eventName] = callback;  // 添加临时事件 要覆盖之前的事件
        }
        else {
            tempMsgDic.Add(eventName, callback);
        }
    }


    public void PostTempEvent(string eventName, System.Object arg = null) {
        if (tempMsgDic.ContainsKey(eventName)) {
            tempMsgDic[eventName].Invoke(arg);
            // 执行完之后移除事件
            tempMsgDic[eventName] = null;
            tempMsgDic.Remove(eventName);
        }
    }

}
