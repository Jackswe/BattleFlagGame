using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



// �û����ƹ�����  ���̲���  ��������
public class UserInputManager
{


    public void Update() {
        // ���������
        if (Input.GetMouseButtonDown(0)) {

            if (EventSystem.current.IsPointerOverGameObject()) {
                // �����UI
            }
            else{
                Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition, (Collider2D col) => {
                    if (col != null) {
                        // ��ʾ�����������
                        GameApp.MessageCenter.PostEvent(col.gameObject,Defines.OnSelectEvent);
                    }
                    else
                    {
                        // ִ��δѡ��
                        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);

                    }
                });
            
            }
        }    
    }
}
