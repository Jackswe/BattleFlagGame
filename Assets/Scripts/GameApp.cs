using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


// 同意定义游戏中的管理器 在这个类执行初始化
public class GameApp : Singleton<GameApp>
{

    public static SoundManager SoundManager; // 定义音频管理器

    public static ControllerManager ControllerManager;  // 控制器管理器

    public static ViewManager ViewManager;  // 视图管理器

    public static ConfigManager ConfigManager; // 配置表管理器

    public static CameraManager CameraManager;  // 摄像机

    public static MessageCenter MessageCenter;  // 消息监听中心

    public static TimerManager TimerManager; // 计时器管理器

    public static FightWorldManager FightManager;  //

    public static MapManager MapManager;

    public static GameDataManager GameDataManager;

    public static UserInputManager UserInputManager;  // 用户输入管理器

    public static CommandManager CommandManager;   // 命令管理器

    public static SkillManager SkillManager;  // 技能管理器


    public override void Init()
    {
        UserInputManager = new UserInputManager();
        TimerManager = new TimerManager();
        MessageCenter = new MessageCenter();
        CameraManager = new CameraManager();
        SoundManager = new SoundManager();
        ConfigManager = new ConfigManager();
        ControllerManager = new ControllerManager();
        FightManager = new FightWorldManager();
        MapManager = new MapManager();
        ViewManager = new ViewManager();
        CommandManager = new CommandManager();
        GameDataManager = new GameDataManager();
        SkillManager = new SkillManager();
    }


    public override void Update(float dt)
    {
        UserInputManager.Update();
        TimerManager.OnUpdate(dt);
        FightManager.Update(dt);
        CommandManager.Update(dt);
        SkillManager.Update(dt);
    }
}
