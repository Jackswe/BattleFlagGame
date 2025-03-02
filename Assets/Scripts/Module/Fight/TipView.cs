using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


// 简单的 tip界面
public class TipView : BaseView
{
    public override void Open(params object[] args)
    {
        base.Open(args);
        Find<Text>("content/txt").text = args[0].ToString();
        Sequence seq = DOTween.Sequence();
        seq.Append(Find("content").transform.DOScale(1,0.15f)).SetEase(Ease.OutBack);  // 
        seq.AppendInterval(0.75f);
        seq.Append(Find("content").transform.DOScale(0, 0.15f)).SetEase(Ease.Linear);
        seq.AppendCallback(() => {

            GameApp.ViewManager.Close(ViewId);
        });
    }
}
