using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ������������㷨
public class _BFS
{

    // ������
    public class Point { 
        public int RowIndex;  // ������
        public int ColIndex;    // ������
        public Point Father;  // ���ڵ� ���ڲ���·��

        public Point(int row, int col) { 
            this.RowIndex = row;
            this.ColIndex = col;
        }

        public Point(int rowIndex, int colIndex, Point father) : this(rowIndex, colIndex)
        {
            this.Father = father;
        }
    }

    public int RowCount;  // ������
    public int ColCount;  // ������

    public Dictionary<string, Point> finds;   // �洢���ҵ��ĵ���ֵ� ��key:�������ƴ�ӵ��ַ�����value�������㣩


    public _BFS(int row,int col) {
        finds = new Dictionary<string, Point>();
        this.RowCount = row;
        this.ColCount = col;
    }


    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="row">��ʼ���������</param>
    /// <param name="colm">��ʼ���������</param>
    /// <param name="step">����</param>
    /// <returns></returns>
    public List<Point> Search(int row, int col, int step) {
        // ������������
        List<Point> searchs = new List<Point>();
        // ��ʼ��
        Point startPoint = new Point(row, col);
        // ����ʼ��洢���������ϵ�
        searchs.Add(startPoint);
        // ��ʼ��Ĭ�Ͽ�ʼ�Ѿ��ҵ����洢���Ѿ��ҵ����ֵ�
        finds.Add($"{row}_{col}",startPoint);

        // �������� �൱�ڿ��������Ĵ���
        for(int i = 0; i < step; i++) {
            // ����һ����ʱ�ļ��� ���ڴ洢Ŀǰ�ҵ����������ĵ�
            List<Point> temps = new List<Point>();
            // ������������
            for(int j = 0;j < searchs.Count;j++)
            {
                Point current = searchs[j];
                // ���ҵ�ǰ�������Χ�ĵ�
                FindAroundPoints(current,temps);
            }
            if(temps.Count == 0)
            {
                // ��ʱ������һ���㶼û�� ���ʾ�൱����·�� û��Ҫ����������
                break;
            }
            // �����ļ���Ҫ���
            searchs.Clear();
            // ����ʱ��ϵĵ���ӵ��������ϵ���
            searchs.AddRange(temps);   
        }

        // �����ҵ��ĵ�ת���ɼ��Ϸ���
        return finds.Values.ToList();

    }


    public void FindAroundPoints(Point current, List<Point> temps) {
        // �м�һ ����
        if (current.RowIndex - 1 >= 0) {
            AddFinds(current.RowIndex - 1,current.ColIndex,current,temps);
        }
        // �� + 1 ���²���
        if (current.RowIndex + 1 < RowCount) { 
            AddFinds(current.RowIndex + 1,current.ColIndex,current,temps);

        }
        // �� -1 �������
        if (current.ColIndex - 1 >= 0) { 
            AddFinds(current.RowIndex,current.ColIndex - 1,current,temps);

        }
        // �� + 1 ���Ҳ���
        if (current.ColIndex + 1 < ColCount)
        {
            AddFinds(current.RowIndex, current.ColIndex + 1, current, temps);

        }
    }

    // ��ӵ����ҵ����ֵ���
    public void AddFinds(int row, int col, Point father, List<Point> temps) {

        // ���ڲ��Ҽ��ϵĽڵ� ���Ҷ�Ӧ��ͼ�������Ͳ����ϰ�����ܼ�������ֵ䵱��
        if (finds.ContainsKey($"{row}_{col}") == false && GameApp.MapManager.GetBlockType(row,col) != BlockType.Obstacle) {
            Point p = new Point(row,col,father);
            // ��ӵ����ҵ����ֵ���
            finds.Add($"{row}_{col}",p);
            // ��ӵ���ʱ���� ������һ�μ�������
            temps.Add(p);
        }
    }



    // Ѱ�ҿ��ƶ��ĵ� �����յ�����ĵ��·��
    public List<Point> FindMinPath(ModelBase model, int step, int endRowIndex, int endColIndex) {
        List<Point> results = Search(model.RowIndex, model.ColIndex, step); // ��ȡ���ƶ��ĵ�ļ���
        if(results.Count == 0)
        {
            return null;
        }
        else { 
            Point minPoint = results[0];  // Ĭ��һ����Ϊ����Ŀ�������
            int mid_dis = Mathf.Abs(minPoint.RowIndex - endRowIndex) + Mathf.Abs(minPoint.ColIndex - endColIndex);
            
            for (int i = 1; i < results.Count; i++) {
                int temp_dis = Mathf.Abs(results[i].RowIndex - endRowIndex) + Mathf.Abs(results[i].ColIndex - endColIndex);
                if (temp_dis < mid_dis) {
                    mid_dis = temp_dis;
                    minPoint = results[i];
                }
            }

            List<Point> paths = new List<Point>();
            Point current = minPoint.Father;
            paths.Add(minPoint);
            while (current != null) {
                paths.Add(current);
                current = current.Father;
            }
            paths.Reverse();  //
            return paths;
        }
    }

}
