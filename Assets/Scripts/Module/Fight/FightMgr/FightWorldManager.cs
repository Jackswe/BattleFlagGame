using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 战斗中的状态枚举
public enum GameState { 
    Idle,
    Enter,
    Player,
    Enemy,
    GameOver
}



// 战斗管理器 用于管理战斗相关的实体（敌人 英雄 地图 格子等）
public class FightWorldManager
{
    public GameState state = GameState.Idle;

    private FightUnitBase current;  // 当前所处的战斗单元格

    public List<Hero> heros;  // 战斗中的英雄集合

    public List<Enemy> enemys;   // 战斗中的敌人集合

    public int RoundCount;   //  回合数

    public FightUnitBase Current => current;

    public FightWorldManager() {
        heros = new List<Hero>();
        enemys = new List<Enemy>();
        // 改变角色状态
        ChangeState(GameState.Idle);
    }


    public void Update(float dt) {

        if (current != null && current.Update(dt) == true)
        {
            // todo
        }
        else {
            current = null;
        }
    }


    // 切换战斗状态
    public void ChangeState(GameState state) { 

        FightUnitBase _current = current;
        this.state = state;
        switch (this.state) { 
            case GameState.Idle:
                // 待机状态
                _current = new FightIdle();
                break;
            case GameState.Enter:
                // 待机状态
                _current = new FightEnter();
                break;
            case GameState.Player:
                // 玩家回合
                _current = new FightPlayerUnit();
                break;
            case GameState.Enemy:
                _current = new FightEnemyUnit();
                break;
            case GameState.GameOver:
                _current = new FightGameOverUnit();
                break;
        }
        _current.Init();


    }


    // 进入战斗 初始化 敌人信息 回合数等
    public void EnterFight() {
        RoundCount = 1;
        heros = new List<Hero>();
        enemys  = new List<Enemy>();
        // 将场景中的敌人脚本进行存储
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");  // 给怪物添加Enemy标签
        Debug.Log("enemy:" + objs.Length);
        for (int i = 0; i < objs.Length; i++) {

            Enemy enemy = objs[i].GetComponent<Enemy>();
            // 当前位置被占用 设置格子类型为障碍物
            GameApp.MapManager.ChangeBlockType(enemy.RowIndex,enemy.ColIndex,BlockType.Obstacle);
            enemys.Add(objs[i].GetComponent<Enemy>());
        }
    
    }


    // 添加英雄
    public void AddHero(Block b,Dictionary<string,string> data) {
        GameObject obj = Object.Instantiate(Resources.Load($"Model/{data["Model"]}")) as GameObject;
        // 设置英雄位置
        obj.transform.position = new Vector3(b.transform.position.x,b.transform.position.y,-1);
        Hero hero = obj.AddComponent<Hero>();
        hero.Init(data,b.RowIndex,b.ColIndex);
        // 这个位置被占领了，设置方块的类型为障碍物
        b.Type = BlockType.Obstacle; 
        heros.Add(hero);
    }


    public void RemoveEnemy(Enemy enemy) {
        enemys.Remove(enemy);

        GameApp.MapManager.ChangeBlockType(enemy.RowIndex,enemy.ColIndex,BlockType.Null);  // 死亡后不要占用格子

        if (enemys.Count == 0) {
            ChangeState(GameState.GameOver);
        }
    }


    public void RemoveHero(Hero hero)
    {
        heros.Remove(hero);

        GameApp.MapManager.ChangeBlockType(hero.RowIndex, hero.ColIndex, BlockType.Null);  // 死亡后不要占用格子
        if (heros.Count == 0)
        {
            ChangeState(GameState.GameOver);
        }
    }


    // 重置英雄行动
    public void ResetHeros() {
        for (int i = 0; i < heros.Count; i++) {
            heros[i].IsStop = false;
        }
    }


    // 重置敌人行动
    public void ResetEnemys()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].IsStop = false;
        }
    }

    // 获得距离目标最近的英雄
    public ModelBase GetMinDisHero(ModelBase model) {
        if (heros.Count == 0) {
            return null;
        }
        Hero hero = heros[0];
        float min_dis = hero.GetDis(model);
        for (int i = 0; i < heros.Count; i++) {
            float dis = heros[i].GetDis(model);
            if (dis < min_dis) {
                min_dis = dis;
                hero = heros[i];
            }
        }
        return hero;
    }

    // 卸载资源
    public void ReLoadRes() { 
        heros.Clear();
        enemys.Clear();
        GameApp.MapManager.Clear();  
    }


}
