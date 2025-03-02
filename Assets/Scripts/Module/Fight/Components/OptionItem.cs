using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


// 选项
public class OptionItem : MonoBehaviour
{
    OptionData op_data;

    public void Init(OptionData data) {
        op_data = data;
    }


    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            GameApp.MessageCenter.PostTempEvent(op_data.EventName);   //  执行配置表中的设置的Event事件
            GameApp.ViewManager.Close((int)ViewType.SelectOptionView);  // 关闭选项界面

        });
        transform.Find("txt").GetComponent<Text>().text = op_data.Name;
    }
}
