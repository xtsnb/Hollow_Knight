using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    // 存储每种音量类型的最小音量值数组
    public int[] minVolume;
    // 显示音量值的文本数组
    public Text[] volText;
    // 音频混合器，用于设置不同的音量
    public AudioMixer audioMixer;

    // 设置主音量
    public void SetMasterVolume(float volume)
    {
        float value;
        // 根据输入的音量值计算混合器中实际的音量值
        value = minVolume[0] / 10 * (10 - volume);  // min=-60, -60 ~ 0
        // 设置音频混合器的主音量
        audioMixer.SetFloat("MasterVolume", value);
        // 更新音量显示文本
        volText[0].text = volume.ToString();
    }

    // 设置音效音量
    public void SetSoundVolume(float volume)
    {
        float value;
        // 根据输入的音量值计算混合器中实际的音量值
        value = minVolume[1] / 10 * (10 - volume);
        // 设置音频混合器的音效音量
        audioMixer.SetFloat("SoundVolume", value);
        // 更新音量显示文本
        volText[1].text = volume.ToString();
    }

    // 设置音乐音量
    public void SetMusicVolume(float volume)
    {
        float value;
        // 根据输入的音量值计算混合器中实际的音量值
        value = minVolume[2] / 10 * (10 - volume);
        // 设置音频混合器的音乐音量
        audioMixer.SetFloat("MusicVolume", value);
        // 更新音量显示文本
        volText[2].text = volume.ToString();
    }
}
