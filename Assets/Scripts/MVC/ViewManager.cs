using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

// ��ͼ��Ϣ��
public class ViewInfo {

    public string PrefabName;   // ��ͼԤ��������
    public Transform parentTf;  // ���ڵĸ���
    public BaseController controller;   // ��ͼ����������
    public int Sorting_Order;  // ��ʾ�㼶  �ı���ʾ˳��
}


// ��ͼ������
public class ViewManager
{
    public Transform canvasTf;  // �������
    public Transform worldCanvasTf;  // ���续�����
    Dictionary<int, IBaseView> _opens; // �����е���ͼ
    Dictionary<int, IBaseView> _viewCache; // ��ͼ����

    Dictionary<int, ViewInfo> _views; // ע�����ͼ��Ϣ

    public ViewManager() {
        canvasTf = GameObject.Find("Canvas").transform;
        worldCanvasTf = GameObject.Find("WorldCanvas").transform;
        _opens = new Dictionary<int, IBaseView>();
        _views = new Dictionary<int, ViewInfo>();
        _viewCache = new Dictionary<int, IBaseView>();


    }

    // ע����ͼ��Ϣ
    public void Register(int key, ViewInfo viewInfo) { 
        if(_views.ContainsKey(key) == false)
        {
            _views.Add(key, viewInfo);
        }
    }

    public void Register(ViewType viewType, ViewInfo viewInfo) {
        Register((int)viewType,viewInfo);
    }


    // �Ƴ���ͼ��Ϣ
    public void UnRegister(int key)
    {
        if (_views.ContainsKey(key)) { 
            _views.Remove(key);
        }
    }

    // �Ƴ����
    public void RemoveView(int key) {
        _views.Remove(key);
        _viewCache.Remove(key);
        _opens.Remove(key);
    }

    // �Ƴ��������е������ͼ
    public void RemoveViewByController(BaseController ctl) {
        foreach (var item in _views) { 
            if(item.Value.controller == ctl)
            {
                // �Ƴ���������������������ͼ
                RemoveView(item.Key);
            }
        }
    }

    // �Ƿ�����
    public bool IsOpen(int key) { 
        return _opens.ContainsKey(key);
    }

    // ��ȡĳ����ͼ
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

    // ������ͼ
    public void Destroy(int key) {
        IBaseView oldView = GetView(key);
        if(oldView != null)
        {
            UnRegister(key);
            oldView.DestroyView();
            _viewCache.Remove(key);
        }
    }


    // �ر����
    public void Close(int key, params object[] args) {
        // û�д�
        if (IsOpen(key) == false) {
            return;
        }
        IBaseView view = GetView(key);
        // ������� �����ѿ���������ɾ��
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


    // �����
    public void Open(ViewType type, params object[] args) {
        Open((int)type,args);
    }

    public void Open(int key, params object[] args) {
        IBaseView view = GetView(key);
        ViewInfo viewInfo = _views[key];
        if (view == null) {
            // ��ʾ��������ͼ ������Դ����
            string type = ((ViewType)key).ToString(); // ���͵��ַ����ͽű����ƶ�Ӧ
            // ���ض�Ӧ��UIԤ������Դ
            GameObject uiObj = UnityEngine.Object.Instantiate(Resources.Load($"View/{viewInfo.PrefabName}"), viewInfo.parentTf) as GameObject;
            Canvas canvas = uiObj.GetComponent<Canvas>();
            if(canvas == null)
            {
                // ��ʾû���ҵ�������� ���ֶ���ӻ������
                canvas = uiObj.AddComponent<Canvas>();
            }
            // ��ȡͼ�����߼����� ���ں��������¼�
            if(uiObj.GetComponent<GraphicRaycaster>() == null) { 
                // û�����ֶ����
                uiObj.AddComponent<GraphicRaycaster>();
            }
            canvas.overrideSorting = true;  // �������ò㼶
            canvas.sortingOrder = viewInfo.Sorting_Order; // ���ò㼶


            view = uiObj.AddComponent(Type.GetType(type)) as IBaseView;  // ��Ӷ�ӦView�ű�
            view.ViewId = key;  // ��ͼid
            view.Controller = viewInfo.controller;  // ���ÿ�����
            // ��ӵ���ͼ����
            _viewCache.Add(key,view);
            viewInfo.controller.OnLoadView(view);
        }

        if(this._opens.ContainsKey(key) == true)
        {
            // �����������ͼ�ļ����а��������ͼ ��ʾ�Ѿ�����
            return;
        }
        this._opens.Add(key, view);

        if (view.IsInit())
        {
            // ��ʾ�Ѿ����г�ʼ����
            view.SetVisible(true); // ��ʾ
            view.Open(args);  // ��
            viewInfo.controller.OpenView(view);
        }
        else {
            // û�н��г�ʼ��
            view.InitUI();
            view.InitData();
            view.Open(args);
            viewInfo.controller.OpenView(view);

        }
    }


    public void ShowHitNum(string num,Color color,Vector3 pos) {
        GameObject obj = UnityEngine.Object.Instantiate(Resources.Load("View/HitNum"), worldCanvasTf) as GameObject;
        obj.transform.position = pos;
        // ����DOTween����
        obj.transform.DOMove(pos + Vector3.up * 1.75f,0.65f).SetEase(Ease.OutBack);
        UnityEngine.Object.Destroy(obj,0.75f);
        Text hitTxt = obj.GetComponent<Text>();
        hitTxt.text = num;
        hitTxt.color = color;
    }


}
