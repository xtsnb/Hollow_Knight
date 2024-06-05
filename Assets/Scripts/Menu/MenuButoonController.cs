using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    // 定义 UI 动画控制器
    public Animator logoTitle;       // 控制 Logo 标题的动画
    public Animator mainMenuScreen;  // 控制主菜单屏幕的动画
    public Animator audioMenuScreen; // 控制音频菜单屏幕的动画
    private AudioSource audioSource; // 用于播放音频的音源组件

    // 在游戏开始时调用的方法
    void Start()
    {
        // 获取当前物体上的 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
    }

    // 更新方法，每帧调用一次
    void Update()
    {
        // 当前未使用
    }

    // 开始游戏的方法
    public void StartGame()
    {
        // 启动协程，延迟显示开场动画
        StartCoroutine(DelayDisplayOpening());
    }

    // 延迟显示开场动画的协程
    IEnumerator DelayDisplayOpening()
    {
        // 播放 Logo 标题和主菜单屏幕的淡出动画
        logoTitle.Play("FadeOut");
        mainMenuScreen.Play("FadeOut");
        // 等待 0.5 秒
        yield return new WaitForSeconds(0.5f);
        // 加载下一个场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // 打开选项菜单的方法
    public void Option()
    {
        // 启动协程，延迟显示音频菜单
        StartCoroutine(DelayDisplayAudioMenu());
    }

    // 延迟显示音频菜单的协程
    IEnumerator DelayDisplayAudioMenu()
    {
        // 播放 Logo 标题的淡出动画和主菜单屏幕的淡出动画
        logoTitle.Play("TitleFadeOut");
        mainMenuScreen.Play("FadeOut");
        // 等待 0.5 秒
        yield return new WaitForSeconds(0.5f);
        // 播放音频菜单屏幕的淡入动画
        audioMenuScreen.Play("FadeIn");
    }

    // 退出游戏的方法
    public void QuitGame()
    {
        // 退出应用程序
        Application.Quit();
    }

    // 关闭音频菜单的方法
    public void QuitAudioMenu()
    {
        // 启动协程，延迟关闭音频菜单
        StartCoroutine(DelayShutAudioMenu());
    }

    // 延迟关闭音频菜单的协程
    IEnumerator DelayShutAudioMenu()
    {
        // 播放音频菜单屏幕的淡出动画
        audioMenuScreen.Play("FadeOut");
        // 等待 0.5 秒
        yield return new WaitForSeconds(0.5f);
        // 播放 Logo 标题的淡入动画和主菜单屏幕的淡入动画
        logoTitle.Play("TitleFadeIn");
        mainMenuScreen.Play("FadeIn");
    }

    // 播放音效的方法
    public void PlayOneShot(AudioClip clip)
    {
        // 使用 AudioSource 的 PlayOneShot 方法播放传入的音频剪辑
        audioSource.PlayOneShot(clip);
    }
}
