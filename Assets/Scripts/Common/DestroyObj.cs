using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �Զ�ɾ������
public class DestroyObj : MonoBehaviour
{
    public float timer;

    private void Start()
    {
        Destroy(gameObject,timer);
    }
}
