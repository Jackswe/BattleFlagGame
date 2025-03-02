using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 命令管理器
public class CommandManager
{
    private Queue<BaseCommand> willDoCommandQueue;  // 将要执行的命令队列
    private Stack<BaseCommand> unDoStack;   // 撤销的命令 栈

    private BaseCommand current;   // 当前所执行的命令

    public CommandManager() { 
        willDoCommandQueue = new Queue<BaseCommand>();  
        unDoStack = new Stack<BaseCommand>();
    }

    // 是否在执行命令
    public bool IsRunningCommand => current != null;

    public void AddCommand(BaseCommand cmd) { 
        willDoCommandQueue.Enqueue(cmd);  // 将命令添加到命令队列中
        unDoStack.Push(cmd);   // 添加到撤销栈中   
    }


    // 每一帧执行
    public void Update(float dt) {
        if (current == null)
        {
            // 如果当前指令为空,则区查找指令队列中是否有没有执行的指令
            if (willDoCommandQueue.Count > 0)
            {
                current = willDoCommandQueue.Dequeue();
                current.Do();
            }
        }
        else {
            if (current.Update(dt) == true) {
                current = null;
            }
        }
    }


    public void Clear() { 
        willDoCommandQueue.Clear(); 
        unDoStack.Clear();
        current = null;
    }

    public void UnDo() { 
        if(unDoStack.Count > 0)
        {
            unDoStack.Pop().UnDo();   // 撤销上一个命令
        }
    }


}
