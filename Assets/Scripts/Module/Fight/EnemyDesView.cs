using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 敌人信息面板
public class EnemyDesView : BaseView
{
    public override void Open(params object[] args)
    {
        base.Open(args);
        Enemy enemy = args[0] as Enemy;
        Find<Image>("bg/icon").SetIcon(enemy.data["Icon"]);  // 设置英雄图标
        Find<Image>("bg/hp/fill").fillAmount = (float)enemy.CurHp / (float)enemy.MaxHp;  // 计算当前血量比例
        Find<Text>("bg/hp/txt").text = $"{enemy.CurHp}/{enemy.MaxHp}";  // 当前血量/总血量
        Find<Text>("bg/atkTxt/txt").text = enemy.Attack.ToString();
        Find<Text>("bg/StepTxt/txt").text = enemy.Step.ToString();


    }
}
