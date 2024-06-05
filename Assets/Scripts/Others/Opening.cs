using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    public VideoPlayer prologue; // 开场序章视频播放器
    public VideoPlayer intro; // 开场介绍视频播放器

    private void Start()
    {
        // 订阅 prologue 视频播放器的循环结束事件
        prologue.loopPointReached += PrologueLoop;
        // 订阅 intro 视频播放器的循环结束事件
        intro.loopPointReached += IntroLoop;
    }

    // 播放序章视频
    public void PlayPrologue()
    {
        prologue.Play();
    }

    // 序章视频播放完成后的回调方法
    private void PrologueLoop(VideoPlayer source)
    {
        // 播放介绍视频
        intro.Play();
    }

    // 介绍视频播放完成后的回调方法
    private void IntroLoop(VideoPlayer source)
    {
        // 加载下一个场景（当前场景索引加一）
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
