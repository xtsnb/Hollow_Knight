using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    // 定义一个 AudioSource 变量，用于播放音频
    private AudioSource audioSource;

    // 在游戏开始时调用的方法
    void Start()
    {
        // 获取当前物体上的 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
    }

    // 播放一个音频剪辑的方法
    public void PlayOneShot(AudioClip audioClip)
    {
        // 使用 AudioSource 的 PlayOneShot 方法播放传入的音频剪辑
        audioSource.PlayOneShot(audioClip);
    }
}
