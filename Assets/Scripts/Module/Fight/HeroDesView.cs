using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 英雄信息面板
public class HeroDesView : BaseView
{
    public override void Open(params object[] args)
    {
        base.Open(args);
        Hero hero = args[0] as Hero;
        Find<Image>("bg/icon").SetIcon(hero.data["Icon"]);  // 设置英雄图标
        Find<Image>("bg/hp/fill").fillAmount = (float)hero.CurHp / (float)hero.MaxHp;  // 计算当前血量比例
        Find<Text>("bg/hp/txt").text = $"{hero.CurHp}/{hero.MaxHp}";  // 当前血量/总血量
        Find<Text>("bg/atkTxt/txt").text = hero.Attack.ToString();
        Find<Text>("bg/StepTxt/txt").text = hero.Step.ToString();


    }
}
