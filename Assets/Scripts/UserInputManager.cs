using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



// 用户控制管理器  键盘操作  鼠标操作等
public class UserInputManager
{


    public void Update() {
        // 点击鼠标左键
        if (Input.GetMouseButtonDown(0)) {

            if (EventSystem.current.IsPointerOverGameObject()) {
                // 点击到UI
            }
            else{
                Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition, (Collider2D col) => {
                    if (col != null) {
                        // 表示点击到了物体
                        GameApp.MessageCenter.PostEvent(col.gameObject,Defines.OnSelectEvent);
                    }
                    else
                    {
                        // 执行未选中
                        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);

                    }
                });
            
            }
        }    
    }
}
