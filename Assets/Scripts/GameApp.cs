using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


// ͬ�ⶨ����Ϸ�еĹ����� �������ִ�г�ʼ��
public class GameApp : Singleton<GameApp>
{

    public static SoundManager SoundManager; // ������Ƶ������

    public static ControllerManager ControllerManager;  // ������������

    public static ViewManager ViewManager;  // ��ͼ������

    public static ConfigManager ConfigManager; // ���ñ������

    public static CameraManager CameraManager;  // �����

    public static MessageCenter MessageCenter;  // ��Ϣ��������

    public static TimerManager TimerManager; // ��ʱ��������

    public static FightWorldManager FightManager;  //

    public static MapManager MapManager;

    public static GameDataManager GameDataManager;

    public static UserInputManager UserInputManager;  // �û����������

    public static CommandManager CommandManager;   // ���������

    public static SkillManager SkillManager;  // ���ܹ�����


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
