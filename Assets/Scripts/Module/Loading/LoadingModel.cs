using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingModel : BaseModel
{
    public string SceneName;
    public System.Action callback;  // 场景加载完成后的回调函数

    public LoadingModel() { 
        
    }

}
