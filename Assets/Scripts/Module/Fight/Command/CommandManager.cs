using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���������
public class CommandManager
{
    private Queue<BaseCommand> willDoCommandQueue;  // ��Ҫִ�е��������
    private Stack<BaseCommand> unDoStack;   // ���������� ջ

    private BaseCommand current;   // ��ǰ��ִ�е�����

    public CommandManager() { 
        willDoCommandQueue = new Queue<BaseCommand>();  
        unDoStack = new Stack<BaseCommand>();
    }

    // �Ƿ���ִ������
    public bool IsRunningCommand => current != null;

    public void AddCommand(BaseCommand cmd) { 
        willDoCommandQueue.Enqueue(cmd);  // ��������ӵ����������
        unDoStack.Push(cmd);   // ��ӵ�����ջ��   
    }


    // ÿһִ֡��
    public void Update(float dt) {
        if (current == null)
        {
            // �����ǰָ��Ϊ��,��������ָ��������Ƿ���û��ִ�е�ָ��
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
            unDoStack.Pop().UnDo();   // ������һ������
        }
    }


}
