using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

// 关卡对象
public class LevelData {
    public int Id;
    public string Name;
    public string SceneName;
    public string Des;
    public bool IsFinish; // 标记是否通关
    public LevelData(Dictionary<string, string> data) { 
        Id = int.Parse(data["Id"]);
        Name = data["Name"];
        SceneName = data["SceneName"];
        Des = data["Des"];
        IsFinish = false;   // 初始化数据对象
    }
}

// 关卡数据
public class LevelModel : BaseModel
{
    private ConfigData levelConfig;
    Dictionary<int, LevelData> levels;  // 关卡字典
    public LevelData current;  // 当前关卡

    public LevelModel() { 
        levels = new Dictionary<int, LevelData>();
    }

    public override void Init()
    {
        levelConfig = GameApp.ConfigManager.GetConfigData("level");
        foreach(var item in levelConfig.GetLines())
        {
            LevelData l_data = new LevelData(item.Value);
            levels.Add(l_data.Id,l_data);
        }
    }

    // 获取关卡信息
    public LevelData GetLevel(int id)
    {
        return levels[id];
    }

}
