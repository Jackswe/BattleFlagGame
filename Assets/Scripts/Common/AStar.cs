using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 路径点类
public class AStarPoint {

    public int RowIndex;
    public int ColIndex;
    public int G;   // 当前节点到开始点的距离
    public int H;  // 当前将节点到终点的距离
    public int F;  // F = G + H
    public AStarPoint Parent;   // 找到当前点的父节点
    public AStarPoint(int row, int col) { 
        this.RowIndex = row; 
        this.ColIndex = col;
        Parent = null;   // 置空父节点

    }


    public AStarPoint(int row, int col, AStarPoint parent) { 
        this.RowIndex = row;
        this.ColIndex = col;
        Parent = parent;
    }

    // 计算G值
    public int GetG() {
        int _g = 0;
        AStarPoint parent = this.Parent;
        while (parent != null) {   // 不断获取上一个节点 直到父节点为空 计算到最开始节点的长度 _g
            _g = _g + 1;
            parent = parent.Parent;
        }

        return _g;
    }

    // 计算H值  当前点到终点的最短距离
    public int GetH(AStarPoint end)
    {
        return Mathf.Abs(RowIndex - end.RowIndex) + Mathf.Abs(ColIndex - end.ColIndex);
    }





}


// A星寻路类
public class AStar
{
    private int rowCount;
    private int colCount;
    private List<AStarPoint> open;  // open表
    private Dictionary<string, AStarPoint> close;  // close表  已经查找过的路径点
    private AStarPoint start;  // 开始点
    private AStarPoint end;         // 终点

    public AStar(int rowCount, int colCount) { 
        this.rowCount = rowCount;
        this.colCount = colCount;
        open = new List<AStarPoint>();
        close = new Dictionary<string, AStarPoint> ();
    }


    //  在open表中查找对应的路径点
    public AStarPoint IsInOpen(int rowIndex, int colIndex) {
        for (int i = 0; i < open.Count; i++) {
            if (open[i].RowIndex == rowIndex && open[i].ColIndex == colIndex) {
                return open[i];
            }
        }
        return null;
    }

    // 某个点是否已经存在close表当中
    public bool isInClose(int rowIndex, int colIndex) {
        if (close.ContainsKey($"{rowIndex}_{colIndex}"))
        {
            return true;
        }
        else {
            return false;

        }
    }


    /*
        A星寻路思路
    1，将起点添加到open表当中
    2，查找open表中 f值最小的路径点
    3， 将找到最小f值的点从open表中移除，并添加到close表中
    4，将当前的路径点上下左右的点添加的open表当中
    5，判断终点是否在open表当中，如果不在 从步骤2继续执行逻辑

     */
    public bool FindPath(AStarPoint start,AStarPoint end,System.Action<List<AStarPoint>> findcallback) {
        this.start = start;
        this.end = end;
        open = new List<AStarPoint> ();
        close = new Dictionary<string, AStarPoint> ();

        // 1，将起点添加到open表当中
        open.Add(start);
        while (true) {
            // 从open表中获取F值最小的路径点
            AStarPoint current = GetMinFFromInOpen();
            if (current == null)
            {
                // 没有路了
                return false;
            }
            else {
                // 3， 将找到最小f值的点从open表中移除，并添加到close表中
                open.Remove(current);
                close.Add($"{current.RowIndex}_{current.ColIndex}",current);
                // 将当前路径点周围的点添加到open表当中
                AddAroundInOpen(current);
                // 判断终点是否在open表中
                AStarPoint endPoint = IsInOpen(end.RowIndex, end.ColIndex);
                if (endPoint != null) {
                    // 找到了路径
                    findcallback(GetPath(endPoint));
                    return true;
                }

                // 将open表排序 按照F从小到大排序
                open.Sort(OpenSort);
            }
        }
    }



    public List<AStarPoint> GetPath(AStarPoint point) {   // 获取到指定点的路径上的所有点
        List<AStarPoint> paths = new List<AStarPoint>();
        paths.Add(point);
        AStarPoint parent = point.Parent;
        while (parent != null) {
            paths.Add(parent);
            parent = parent.Parent;
        }

        // 倒置
        paths.Reverse();   // 
        return paths;
    }


    public int OpenSort(AStarPoint a, AStarPoint b) {
        return a.F - b.F;
    }


    public void AddAroundInOpen(AStarPoint current) {
        // 上下左右
        if (current.RowIndex - 1 >= 0) {
            AddOpen(current, current.RowIndex - 1,current.ColIndex);
        }

        if (current.RowIndex + 1 < rowCount)
        {
            AddOpen(current, current.RowIndex + 1, current.ColIndex);

        }

        if (current.ColIndex - 1 >= 0)
        {
            AddOpen(current, current.RowIndex, current.ColIndex - 1);

        }

        if (current.ColIndex + 1 < colCount)
        {
            AddOpen(current, current.RowIndex, current.ColIndex + 1);

        }
    }


    public void AddOpen(AStarPoint current, int row, int col) {
        // 不再open表中 不在close表中  对应的格子类型不能为障碍物 才能加入到open表中
        if(isInClose(row,col) == false && IsInOpen(row,col) == null && GameApp.MapManager.GetBlockType(row,col) == BlockType.Null) {
            AStarPoint newPoint = new AStarPoint(row,col,current);
            newPoint.G = newPoint.GetG();
            newPoint.H = newPoint.GetH(end);  // 到终点的距离
            newPoint.F = newPoint.G + newPoint.H;
            open.Add(newPoint);
        }
    }



    public AStarPoint GetMinFFromInOpen() {
        // open 表中没有点
        if (open.Count == 0) {
            return null;
        }

        return open[0];  // open表会进行排序 最小f值在第一位 所以返回第一位的路径点
    }


}
