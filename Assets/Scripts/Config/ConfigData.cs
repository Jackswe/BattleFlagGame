using DG.Tweening.Plugins;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// 读取csv格式数据表(逗号隔开)
public class ConfigData
{
    // 每个数据表存储的数据到字典中  key是字典的id，value是每一行数据
    private Dictionary<int, Dictionary<string, string>> datas;

    public string fileName;  // 配置表文件名称
    public ConfigData(string fileName) { 
        this.fileName = fileName;
        this.datas = new Dictionary<int, Dictionary<string, string>>();
    }


    public TextAsset LoadFile() {
        return Resources.Load<TextAsset>($"Data/{fileName}");
    }


    // 读取
    public void Load(string txt)
    {
        string[] dataArr = txt.Split("\n"); // 切出每一行
        string[] titleArr = dataArr[0].Trim().Split(','); // 第一行去除空格按逗号分割
        // 内容从第三行开始读取 （下标从2开始）  第二行是中文注释
        for (int i = 2; i < dataArr.Length; i++)
        {
            string[] tempArr = dataArr[i].Trim().Split(',');
            Dictionary<string,string> tempData = new Dictionary<string, string>();
            for (int j = 0; j < tempArr.Length; j++) {
                tempData.Add(titleArr[j], tempArr[j]);
            }
            datas.Add(int.Parse(tempData["Id"]),tempData);
        }

    }


    public Dictionary<string,string> GetDataById(int id)
    {
        if (datas.ContainsKey(id)) {
            return datas[id];
        }
        return null;
    }

    public Dictionary<int, Dictionary<string, string>> GetLines() {
        return datas;
    }


}
