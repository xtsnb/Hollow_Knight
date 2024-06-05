using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    public SpriteRenderer render; // 精灵渲染器
    public Color normalColor; // 正常颜色
    public Color flashColor; // 闪烁颜色
    public int duration; // 无敌持续时间
    public bool isInvincible; // 是否处于无敌状态

    // 设置无敌状态的协程方法
    public IEnumerator SetInvincibility()
    {
        isInvincible = true; // 标记为无敌状态
        for (int i = 0; i < duration; i++) // 持续 duration 次循环
        {
            yield return new WaitForSeconds(0.1f); // 等待 0.1 秒
            render.color = flashColor; // 设置闪烁颜色
            yield return new WaitForSeconds(0.1f); // 等待 0.1 秒
            render.color = normalColor; // 恢复正常颜色
        }
        isInvincible = false; // 结束无敌状态
    }
}
