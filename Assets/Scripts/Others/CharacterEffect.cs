using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem fallTrail; // 下落拖尾特效
    [SerializeField] ParticleSystem burstRocks; // 爆炸岩石特效
    [SerializeField] ParticleSystem lowHealth; // 低血量特效
    [SerializeField] ParticleSystem hitL; // 左侧受击特效
    [SerializeField] ParticleSystem hitR; // 右侧受击特效
    [SerializeField] ParticleSystem dustJump; // 跳跃起尘特效

    // 特效类型枚举
    public enum EffectType
    {
        FallTrail, BurstRocks, LowHealth, HitL, HitR, DustJump
    }

    // 执行特效方法，根据特效类型和是否启用进行处理
    public void DoEffect(EffectType effectType, bool enabled)
    {
        switch (effectType)
        {
            case EffectType.FallTrail:
                if (enabled)
                    fallTrail.Play(); // 播放下落拖尾特效
                else
                    fallTrail.Stop(); // 停止下落拖尾特效
                break;
            case EffectType.BurstRocks:
                if (enabled)
                    burstRocks.Play(); // 播放爆炸岩石特效
                else
                    burstRocks.Stop(); // 停止爆炸岩石特效
                break;
            case EffectType.LowHealth:
                if (enabled)
                    lowHealth.Play(); // 播放低血量特效
                else
                    lowHealth.Stop(); // 停止低血量特效
                break;
            case EffectType.HitL:
                if (enabled)
                    hitL.Play(); // 播放左侧受击特效
                else
                    hitL.Stop(); // 停止左侧受击特效
                break;
            case EffectType.HitR:
                if (enabled)
                    hitR.Play(); // 播放右侧受击特效
                else
                    hitR.Stop(); // 停止右侧受击特效
                break;
            case EffectType.DustJump:
                if (enabled)
                    dustJump.Play(); // 播放跳跃起尘特效
                else
                    dustJump.Stop(); // 停止跳跃起尘特效
                break;
            default:
                break;
        }
    }
}
