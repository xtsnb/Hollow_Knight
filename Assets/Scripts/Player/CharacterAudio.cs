using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField] AudioSource mainAudio; // 主音频源
    [SerializeField] AudioSource jumpAudio; // 跳跃音频源
    [SerializeField] AudioSource landingAudio; // 着陆音频源
    [SerializeField] AudioSource fallingAudio; // 下落音频源
    [SerializeField] AudioSource takeDamageAudio; // 受伤音频源

    // 音频类型枚举
    public enum AudioType
    {
        Jump, Landing, Falling, TakeDamage
    }

    // 播放音频方法，根据音频类型和播放状态进行处理
    public void Play(AudioType audioType, bool playState)
    {
        AudioSource audioSource = null;
        // 根据音频类型选择对应的音频源
        switch (audioType)
        {
            case AudioType.Jump:
                audioSource = jumpAudio;
                break;
            case AudioType.Landing:
                audioSource = landingAudio;
                break;
            case AudioType.Falling:
                audioSource = fallingAudio;
                break;
            case AudioType.TakeDamage:
                audioSource = takeDamageAudio;
                break;
        }
        // 如果找到了对应的音频源
        if (audioSource != null)
        {
            // 根据播放状态播放或停止音频
            if (playState)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }
    }
}
