using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ѡ�ؿ���������򵥵Ŀ������ű�
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2.5f;  // �ƶ��ٶ�
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        GameApp.CameraManager.SetPos(transform.position);  // ���������λ��
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
            pos.x = Mathf.Clamp(pos.x,-32,24);  //��֤��ɫֻ�ڵ�·��
            transform.position = pos; // �ƶ���ɫ
            
            GameApp.CameraManager.SetPos(transform.position);  // ���������λ��  ʹ��һֱ�����ɫ


            animator.Play("move");
        }
    }

    public void Flip() { 
        // ��ɫ��ת
        Vector3 scale = transform.localScale;  // ��ȡ��������
        scale.x *= -1;  // ��ת
        transform.localScale = scale;
       
    }


}
