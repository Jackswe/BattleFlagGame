using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 选关卡界面任务简单的控制器脚本
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2.5f;  // 移动速度
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        GameApp.CameraManager.SetPos(transform.position);  // 设置摄像机位置
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h == 0)
        {
            animator.Play("idle");
        }
        else {
            if ((h > 0 && transform.localScale.x < 0) || (h <0 && transform.localScale.x > 0)) {
                Flip();  // 
            }
            // -32 24
            Vector3 pos = transform.position + Vector3.right * h * moveSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x,-32,24);  //保证角色只在道路上
            transform.position = pos; // 移动角色
            
            GameApp.CameraManager.SetPos(transform.position);  // 设置摄像机位置  使其一直看向角色


            animator.Play("move");
        }
    }

    public void Flip() { 
        // 角色翻转
        Vector3 scale = transform.localScale;  // 获取本地缩放
        scale.x *= -1;  // 翻转
        transform.localScale = scale;
       
    }


}
