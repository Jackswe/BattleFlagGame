using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


// 声音管理器
public class SoundManager
{
    private AudioSource bgmSource;  // 播放bgm的音频组件

    private Dictionary<string, AudioClip> clips;  // 音频缓存字典

    private bool isStop;  // 是否静音

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


    private float bgmVolume; //  bgm声音大小

    public float BgmVolume { 
        get { 
            return bgmVolume; 
        } 
        
        set{
            bgmVolume = value;
            bgmSource.volume = bgmVolume;   // 调整实际的声音大小
        }
    }

    private float effectVolume; // 音效大小 （攻击 受伤害等音效）

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
        // 设置初始值
        IsStop = false;
        BgmVolume = 1;
        EffectVolume = 1;  // 最大

    }

    // 播放bgm
    public void PlayBGM(string res) { 

        if(isStop == true)
        {
            return;   // 如果停止播放则直接返回
        }



        if(clips.ContainsKey(res) == false)
        {
            // 没有当前音频 加载音频
            AudioClip clip = Resources.Load<AudioClip>($"Sounds/{res}");
            clips.Add(res, clip );

        }
        bgmSource.clip = clips[res];
        // 播放切片
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
