using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Ӣ����Ϣ���
public class HeroDesView : BaseView
{
    public override void Open(params object[] args)
    {
        base.Open(args);
        Hero hero = args[0] as Hero;
        Find<Image>("bg/icon").SetIcon(hero.data["Icon"]);  // ����Ӣ��ͼ��
        Find<Image>("bg/hp/fill").fillAmount = (float)hero.CurHp / (float)hero.MaxHp;  // ���㵱ǰѪ������
        Find<Text>("bg/hp/txt").text = $"{hero.CurHp}/{hero.MaxHp}";  // ��ǰѪ��/��Ѫ��
        Find<Text>("bg/atkTxt/txt").text = hero.Attack.ToString();
        Find<Text>("bg/StepTxt/txt").text = hero.Step.ToString();


    }
}
