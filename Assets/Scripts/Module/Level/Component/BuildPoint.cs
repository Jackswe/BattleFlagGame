using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    public int LevelId;  // ���ùؿ�Id

    public void OnTriggerEnter2D(Collider2D collider) {
        GameApp.MessageCenter.PostEvent(Defines.ShowLevelDesEvent, LevelId);
    }


    private void OnTriggerExit2D(Collider2D collider) {
        GameApp.MessageCenter.PostEvent(Defines.HideLevelDesEvent);
    }
}
