using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �̳�mono�Ľű�
public class GameScene : MonoBehaviour
{
    public Texture2D mouseTxt; // ���ͼƬ
    float dt;
    private bool isLoaded = false;

    public void Awake()
    {
        if (isLoaded == true)
        {
            Destroy(gameObject);
        }
        else { 
            isLoaded = true;
            DontDestroyOnLoad(gameObject);  // ����ȷ��һ����Ϸ�����ڼ����³���ʱ���ᱻ���١�
            GameApp.Instance.Init();

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // ���������ʽ
        Cursor.SetCursor(mouseTxt,Vector2.zero,CursorMode.Auto);

        // ע�����ñ�
        RegisterConfigs();

        GameApp.ConfigManager.LoadAllConfigs();  // �����������ñ�

        // ����
        /*ConfigData tempData = GameApp.ConfigManager.GetConfigData("enemy");
        string name = tempData.GetDataById(10001)["Name"];
        Debug.Log(name);*/

        // ���ű�������
        GameApp.SoundManager.PlayBGM("login");

        RegisterModule();   // ע����Ϸ�еĿ�����

        InitModule();   // ִ�����п������ĳ�ʼ������
    }

    // ע�������
    void RegisterModule() {
        GameApp.ControllerManager.Register(ControllerType.GameUI,new GameUIController());
        GameApp.ControllerManager.Register(ControllerType.Game, new GameController());
        GameApp.ControllerManager.Register(ControllerType.Loading, new LoadingController());
        GameApp.ControllerManager.Register(ControllerType.Level, new LevelController());
        GameApp.ControllerManager.Register(ControllerType.Fight,new FightController());
    }



    // ִ�������п������ĳ�ʼ������
    void InitModule() {
        GameApp.ControllerManager.InitAddModules();
    }


    // ע�����ñ�
    void RegisterConfigs() {
        GameApp.ConfigManager.Register("enemy",new ConfigData("enemy"));
        GameApp.ConfigManager.Register("level", new ConfigData("level"));
        GameApp.ConfigManager.Register("option", new ConfigData("option"));
        GameApp.ConfigManager.Register("player", new ConfigData("player"));
        GameApp.ConfigManager.Register("role", new ConfigData("role"));
        GameApp.ConfigManager.Register("skill", new ConfigData("skill"));

    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        GameApp.Instance.Update(dt);
    }
}
