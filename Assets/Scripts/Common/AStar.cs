using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ·������
public class AStarPoint {

    public int RowIndex;
    public int ColIndex;
    public int G;   // ��ǰ�ڵ㵽��ʼ��ľ���
    public int H;  // ��ǰ���ڵ㵽�յ�ľ���
    public int F;  // F = G + H
    public AStarPoint Parent;   // �ҵ���ǰ��ĸ��ڵ�
    public AStarPoint(int row, int col) { 
        this.RowIndex = row; 
        this.ColIndex = col;
        Parent = null;   // �ÿո��ڵ�

    }


    public AStarPoint(int row, int col, AStarPoint parent) { 
        this.RowIndex = row;
        this.ColIndex = col;
        Parent = parent;
    }

    // ����Gֵ
    public int GetG() {
        int _g = 0;
        AStarPoint parent = this.Parent;
        while (parent != null) {   // ���ϻ�ȡ��һ���ڵ� ֱ�����ڵ�Ϊ�� ���㵽�ʼ�ڵ�ĳ��� _g
            _g = _g + 1;
            parent = parent.Parent;
        }

        return _g;
    }

    // ����Hֵ  ��ǰ�㵽�յ����̾���
    public int GetH(AStarPoint end)
    {
        return Mathf.Abs(RowIndex - end.RowIndex) + Mathf.Abs(ColIndex - end.ColIndex);
    }





}


// A��Ѱ·��
public class AStar
{
    private int rowCount;
    private int colCount;
    private List<AStarPoint> open;  // open��
    private Dictionary<string, AStarPoint> close;  // close��  �Ѿ����ҹ���·����
    private AStarPoint start;  // ��ʼ��
    private AStarPoint end;         // �յ�

    public AStar(int rowCount, int colCount) { 
        this.rowCount = rowCount;
        this.colCount = colCount;
        open = new List<AStarPoint>();
        close = new Dictionary<string, AStarPoint> ();
    }


    //  ��open���в��Ҷ�Ӧ��·����
    public AStarPoint IsInOpen(int rowIndex, int colIndex) {
        for (int i = 0; i < open.Count; i++) {
            if (open[i].RowIndex == rowIndex && open[i].ColIndex == colIndex) {
                return open[i];
            }
        }
        return null;
    }

    // ĳ�����Ƿ��Ѿ�����close����
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
        A��Ѱ·˼·
    1���������ӵ�open����
    2������open���� fֵ��С��·����
    3�� ���ҵ���Сfֵ�ĵ��open�����Ƴ�������ӵ�close����
    4������ǰ��·�����������ҵĵ���ӵ�open����
    5���ж��յ��Ƿ���open���У�������� �Ӳ���2����ִ���߼�

     */
    public bool FindPath(AStarPoint start,AStarPoint end,System.Action<List<AStarPoint>> findcallback) {
        this.start = start;
        this.end = end;
        open = new List<AStarPoint> ();
        close = new Dictionary<string, AStarPoint> ();

        // 1���������ӵ�open����
        open.Add(start);
        while (true) {
            // ��open���л�ȡFֵ��С��·����
            AStarPoint current = GetMinFFromInOpen();
            if (current == null)
            {
                // û��·��
                return false;
            }
            else {
                // 3�� ���ҵ���Сfֵ�ĵ��open�����Ƴ�������ӵ�close����
                open.Remove(current);
                close.Add($"{current.RowIndex}_{current.ColIndex}",current);
                // ����ǰ·������Χ�ĵ���ӵ�open����
                AddAroundInOpen(current);
                // �ж��յ��Ƿ���open����
                AStarPoint endPoint = IsInOpen(end.RowIndex, end.ColIndex);
                if (endPoint != null) {
                    // �ҵ���·��
                    findcallback(GetPath(endPoint));
                    return true;
                }

                // ��open������ ����F��С��������
                open.Sort(OpenSort);
            }
        }
    }



    public List<AStarPoint> GetPath(AStarPoint point) {   // ��ȡ��ָ�����·���ϵ����е�
        List<AStarPoint> paths = new List<AStarPoint>();
        paths.Add(point);
        AStarPoint parent = point.Parent;
        while (parent != null) {
            paths.Add(parent);
            parent = parent.Parent;
        }

        // ����
        paths.Reverse();   // 
        return paths;
    }


    public int OpenSort(AStarPoint a, AStarPoint b) {
        return a.F - b.F;
    }


    public void AddAroundInOpen(AStarPoint current) {
        // ��������
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
        // ����open���� ����close����  ��Ӧ�ĸ������Ͳ���Ϊ�ϰ��� ���ܼ��뵽open����
        if(isInClose(row,col) == false && IsInOpen(row,col) == null && GameApp.MapManager.GetBlockType(row,col) == BlockType.Null) {
            AStarPoint newPoint = new AStarPoint(row,col,current);
            newPoint.G = newPoint.GetG();
            newPoint.H = newPoint.GetH(end);  // ���յ�ľ���
            newPoint.F = newPoint.G + newPoint.H;
            open.Add(newPoint);
        }
    }



    public AStarPoint GetMinFFromInOpen() {
        // open ����û�е�
        if (open.Count == 0) {
            return null;
        }

        return open[0];  // open���������� ��Сfֵ�ڵ�һλ ���Է��ص�һλ��·����
    }


}
