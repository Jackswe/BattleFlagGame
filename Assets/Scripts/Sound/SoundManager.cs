using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


// ����������
public class SoundManager
{
    private AudioSource bgmSource;  // ����bgm����Ƶ���

    private Dictionary<string, AudioClip> clips;  // ��Ƶ�����ֵ�

    private bool isStop;  // �Ƿ���

    public bool IsStop {
        get{
            return isStop;
        }
        set
        {
            isStop = value;
            if (isStop == true)
            {
                bgmSource.Pause();
            }
            else { 
                bgmSource.Play();
            }
        }
    }


    private float bgmVolume; //  bgm������С

    public float BgmVolume { 
        get { 
            return bgmVolume; 
        } 
        
        set{
            bgmVolume = value;
            bgmSource.volume = bgmVolume;   // ����ʵ�ʵ�������С
        }
    }

    private float effectVolume; // ��Ч��С ������ ���˺�����Ч��

    public float EffectVolume {
        get { 
            return effectVolume;
        }
        set { 
            effectVolume = value;
        }
    }

    public SoundManager() {
        clips = new Dictionary<string, AudioClip>();
        bgmSource = GameObject.Find("game").GetComponent<AudioSource>();
        // ���ó�ʼֵ
        IsStop = false;
        BgmVolume = 1;
        EffectVolume = 1;  // ���

    }

    // ����bgm
    public void PlayBGM(string res) { 

        if(isStop == true)
        {
            return;   // ���ֹͣ������ֱ�ӷ���
        }



        if(clips.ContainsKey(res) == false)
        {
            // û�е�ǰ��Ƶ ������Ƶ
            AudioClip clip = Resources.Load<AudioClip>($"Sounds/{res}");
            clips.Add(res, clip );

        }
        bgmSource.clip = clips[res];
        // ������Ƭ
        bgmSource.Play();
    }

    public void PlayEffect(string name,Vector3 pos) {
        if (isStop == true) { 
            return;
        }
        AudioClip clip = null;
        if(clips.ContainsKey (name) == false)
        {
            clip = Resources.Load<AudioClip>($"Sounds/{name}");
            clips.Add (name, clip );
        }
        AudioSource.PlayClipAtPoint(clips[name], pos);

    }


}
