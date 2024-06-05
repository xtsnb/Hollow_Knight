using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShot : MonoBehaviour
{
    // 当按钮被按下时调用此方法来播放给定的音频剪辑
    public void ButtonPlayOneShot(AudioClip clip)
    {
        // 在场景中查找第一个 MenuAudio 类的实例，并调用其 PlayOneShot 方法，传递给定的音频剪辑
        FindObjectOfType<MenuAudio>().PlayOneShot(clip);
    }
}
