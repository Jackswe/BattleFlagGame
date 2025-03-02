using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 命令基类  （可派生 移动 使用技能等）
public class BaseCommand
{
    public ModelBase model;   // 命令的对象
    protected bool isFinish;  // 是否做完标记

    public BaseCommand() { 
        
    }

    public BaseCommand(ModelBase model) { 
        this.model = model;
        isFinish = false;
    }

    public virtual bool Update(float dt) { 

        return isFinish;
    }

    // 执行命令
    public virtual void Do() { 
        
    }

    // 撤销
    public virtual void UnDo() { 
        
    }


}
