using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPause : MonoBehaviour
{
    bool waiting = false; // 是否正在等待

    // 暂停游戏一段时间
    public void Stop(float duration, float timeScale)
    {
        // 如果已经在等待中，则直接返回
        if (waiting)
            return;

        // 设置游戏时间缩放
        Time.timeScale = timeScale;
        // 开始等待指定时长
        StartCoroutine(Wait(duration));
    }

    // 等待指定时长的协程
    IEnumerator Wait(float duration)
    {
        waiting = true; // 标记为正在等待
        yield return new WaitForSecondsRealtime(duration); // 等待指定时长
        Time.timeScale = 1.0f; // 恢复游戏时间缩放为正常值
        waiting = false; // 标记为等待结束
    }
}
