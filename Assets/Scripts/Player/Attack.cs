using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attack : MonoBehaviour
{
    // 攻击时使用的不同攻击预制体
     public GameObject slash; // 普通斩击
     public GameObject altSlash; // 替代斩击
     public GameObject downSlash; // 向下斩击
     public GameObject upSlash; // 向上斩击

    // 用于过滤敌人的碰撞器过滤器
    public ContactFilter2D enemyContactFilter;

    // 攻击类型枚举
    public enum AttackType
    {
        Slash, AltSlash, DownSlash, Upslash
    }

    // 播放攻击方法，根据攻击类型和传入的碰撞器列表引用执行相应的攻击
    public void Play(AttackType attackType, ref List<Collider2D> colliders)
    {
        switch (attackType)
        {
            case AttackType.Slash:
                // 通过重叠碰撞器检测攻击目标，并将结果添加到碰撞器列表中
                Physics2D.OverlapCollider(slash.GetComponent<Collider2D>(), enemyContactFilter, colliders);
                // 播放普通斩击的音效
                slash.GetComponent<AudioSource>().Play();
                break;
            case AttackType.AltSlash:
                Physics2D.OverlapCollider(altSlash.GetComponent<Collider2D>(), enemyContactFilter, colliders);
                altSlash.GetComponent<AudioSource>().Play(); // 播放替代斩击的音效
                break;
            case AttackType.DownSlash:
                Physics2D.OverlapCollider(downSlash.GetComponent<Collider2D>(), enemyContactFilter, colliders);
                downSlash.GetComponent<AudioSource>().Play(); // 播放向下斩击的音效
                break;
            case AttackType.Upslash:
                Physics2D.OverlapCollider(upSlash.GetComponent<Collider2D>(), enemyContactFilter, colliders);
                upSlash.GetComponent<AudioSource>().Play(); // 播放向上斩击的音效
                break;
            default:
                break;
        }
    }
}
