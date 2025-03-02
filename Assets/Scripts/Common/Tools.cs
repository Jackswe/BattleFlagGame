using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 工具类
public static class Tools
{

    public static void SetIcon(this Image img, string res) {

        img.sprite = Resources.Load<Sprite>($"Icon/{res}");
    }


    // 检测鼠标位置是否有2d碰撞物体
    public static void ScreenPointToRay2D(Camera cam, Vector2 mousePos, System.Action<Collider2D> callback) { 
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);  // 屏幕坐标转世界坐标

        // 使用 Physics2D.OverlapCircle 方法，在 worldPos 的位置创建一个半径为 0.02 的圆，
        // 并检测该圆范围内是否有 Collider2D 碰撞体存在。
        Collider2D col = Physics2D.OverlapCircle(worldPos, 0.02f);  
        // 如果发生碰撞则调用回调函数
        callback?.Invoke(col);
    }

    // 返回这个碰撞体
    public static Collider2D ScreenPointToRay2D(Camera cam,Vector2 mousePos) {
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);  // 屏幕坐标转世界坐标

        // 使用 Physics2D.OverlapCircle 方法，在 worldPos 的位置创建一个半径为 0.02 的圆，
        // 并检测该圆范围内是否有 Collider2D 碰撞体存在。
        Collider2D col = Physics2D.OverlapCircle(worldPos, 0.02f);

        return col;
    }




}
