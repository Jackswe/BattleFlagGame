using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

// 试图信息类
public class ViewInfo {

    public string PrefabName;   // 试图预制体名称
    public Transform parentTf;  // 所在的父级
    public BaseController controller;   // 试图所属控制器
    public int Sorting_Order;  // 显示层级  改变显示顺序
}


// 试图管理器
public class ViewManager
{
    public Transform canvasTf;  // 画布组件
    public Transform worldCanvasTf;  // 世界画布组件
    Dictionary<int, IBaseView> _opens; // 开启中的试图
    Dictionary<int, IBaseView> _viewCache; // 视图缓存

    Dictionary<int, ViewInfo> _views; // 注册的视图信息

    public ViewManager() {
        canvasTf = GameObject.Find("Canvas").transform;
        worldCanvasTf = GameObject.Find("WorldCanvas").transform;
        _opens = new Dictionary<int, IBaseView>();
        _views = new Dictionary<int, ViewInfo>();
        _viewCache = new Dictionary<int, IBaseView>();


    }

    // 注册视图信息
    public void Register(int key, ViewInfo viewInfo) { 
        if(_views.ContainsKey(key) == false)
        {
            _views.Add(key, viewInfo);
        }
    }

    public void Register(ViewType viewType, ViewInfo viewInfo) {
        Register((int)viewType,viewInfo);
    }


    // 移除视图信息
    public void UnRegister(int key)
    {
        if (_views.ContainsKey(key)) { 
            _views.Remove(key);
        }
    }

    // 移除面板
    public void RemoveView(int key) {
        _views.Remove(key);
        _viewCache.Remove(key);
        _opens.Remove(key);
    }

    // 移除控制器中的面板视图
    public void RemoveViewByController(BaseController ctl) {
        foreach (var item in _views) { 
            if(item.Value.controller == ctl)
            {
                // 移除这个控制器包含的面板视图
                RemoveView(item.Key);
            }
        }
    }

    // 是否开启中
    public bool IsOpen(int key) { 
        return _opens.ContainsKey(key);
    }

    // 获取某个视图
    public IBaseView GetView(int key)
    {
        if (_opens.ContainsKey(key)) {
            return _opens[key];
        }

        if(_viewCache.ContainsKey(key))
        {
            return _viewCache[key];
        }

        return null;
    }


    public T GetView<T>(int key) where T: class,IBaseView {
        IBaseView view = GetView(key);
        if (view != null) {
            return view as T;
        }
        return null;
    }

    // 销毁视图
    public void Destroy(int key) {
        IBaseView oldView = GetView(key);
        if(oldView != null)
        {
            UnRegister(key);
            oldView.DestroyView();
            _viewCache.Remove(key);
        }
    }


    // 关闭面板
    public void Close(int key, params object[] args) {
        // 没有打开
        if (IsOpen(key) == false) {
            return;
        }
        IBaseView view = GetView(key);
        // 如果存在 则在已开启集合中删除
        if (view != null) { 
            _opens.Remove(key);
            view.Close(args);
            _views[key].controller.CLoseView(view);
        }
    }


    public void CloseAll() {
        List<IBaseView> list = _opens.Values.ToList();
        for (int i = list.Count - 1; i >= 0; i--) {
            Close(list[i].ViewId);
        }
    }


    // 打开面板
    public void Open(ViewType type, params object[] args) {
        Open((int)type,args);
    }

    public void Open(int key, params object[] args) {
        IBaseView view = GetView(key);
        ViewInfo viewInfo = _views[key];
        if (view == null) {
            // 表示不存在视图 进行资源加载
            string type = ((ViewType)key).ToString(); // 类型的字符串和脚本名称对应
            // 加载对应的UI预制体资源
            GameObject uiObj = UnityEngine.Object.Instantiate(Resources.Load($"View/{viewInfo.PrefabName}"), viewInfo.parentTf) as GameObject;
            Canvas canvas = uiObj.GetComponent<Canvas>();
            if(canvas == null)
            {
                // 表示没有找到画布组件 则手动添加画布组件
                canvas = uiObj.AddComponent<Canvas>();
            }
            // 获取图像射线检测组件 用于后续处理事件
            if(uiObj.GetComponent<GraphicRaycaster>() == null) { 
                // 没有则手动添加
                uiObj.AddComponent<GraphicRaycaster>();
            }
            canvas.overrideSorting = true;  // 可以设置层级
            canvas.sortingOrder = viewInfo.Sorting_Order; // 设置层级


            view = uiObj.AddComponent(Type.GetType(type)) as IBaseView;  // 添加对应View脚本
            view.ViewId = key;  // 视图id
            view.Controller = viewInfo.controller;  // 设置控制器
            // 添加到视图缓存
            _viewCache.Add(key,view);
            viewInfo.controller.OnLoadView(view);
        }

        if(this._opens.ContainsKey(key) == true)
        {
            // 如果开启的视图的集合中包含这个视图 表示已经打开了
            return;
        }
        this._opens.Add(key, view);

        if (view.IsInit())
        {
            // 表示已经进行初始化了
            view.SetVisible(true); // 显示
            view.Open(args);  // 打开
            viewInfo.controller.OpenView(view);
        }
        else {
            // 没有进行初始化
            view.InitUI();
            view.InitData();
            view.Open(args);
            viewInfo.controller.OpenView(view);

        }
    }


    public void ShowHitNum(string num,Color color,Vector3 pos) {
        GameObject obj = UnityEngine.Object.Instantiate(Resources.Load("View/HitNum"), worldCanvasTf) as GameObject;
        obj.transform.position = pos;
        // 设置DOTween动画
        obj.transform.DOMove(pos + Vector3.up * 1.75f,0.65f).SetEase(Ease.OutBack);
        UnityEngine.Object.Destroy(obj,0.75f);
        Text hitTxt = obj.GetComponent<Text>();
        hitTxt.text = num;
        hitTxt.color = color;
    }


}
