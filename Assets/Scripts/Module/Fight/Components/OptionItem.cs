using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


// ѡ��
public class OptionItem : MonoBehaviour
{
    OptionData op_data;

    public void Init(OptionData data) {
        op_data = data;
    }


    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            GameApp.MessageCenter.PostTempEvent(op_data.EventName);   //  ִ�����ñ��е����õ�Event�¼�
            GameApp.ViewManager.Close((int)ViewType.SelectOptionView);  // �ر�ѡ�����

        });
        transform.Find("txt").GetComponent<Text>().text = op_data.Name;
    }
}
