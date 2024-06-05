using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] protected int health; // 物体的生命值

    protected bool isDead; // 标记物体是否死亡

    // 检查物体是否死亡
    protected void CheckIsDead()
    {
        if (health <= 0 && !isDead) // 如果生命值小于等于0且物体未死亡
        {
            Dead(); // 调用死亡方法
        }
    }

    // 处理受到伤害的方法
    public virtual void Hurt(int damage)
    {
        if (!isDead) // 如果物体未死亡
        {
            health -= damage; // 减少生命值
        }
    }

    // 处理受到伤害的方法，带有攻击位置参数（重载）
    public virtual void Hurt(int damage, Transform attackPosition)
    {
        if (!isDead) // 如果物体未死亡
        {
            health -= damage; // 减少生命值
        }
    }

    // 物体死亡的方法
    protected virtual void Dead()
    {
        isDead = true; // 标记物体为死亡
    }
}
